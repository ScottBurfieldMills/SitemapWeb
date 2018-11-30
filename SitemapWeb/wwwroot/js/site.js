if (document.getElementById('sitemap-checker') !== undefined) {
    Vue.component('sitemap-submit', {
        template: '#sitemap-submit',
        data: function () {
            return {
                url: '',
                submitting: false
            };
        },
        methods: {
            submit: function () {
                var self = this;

                self.submitting = true;

                axios.post(self.$el.action, {
                    sitemapUrl: self.url
                }).then((response) => {
                    self.submitting = false;

                    self.$emit('submitted', response.data);
                }).catch((response) => {
                    self.submitting = false;
                });
            }
        }
    });

    class SitemapHub {
        watch(submittionId, updateCallback) {
            this.connection = new signalR.HubConnectionBuilder()
                .withUrl('/sitemaphub')
                .build();

            this.connection.on('SubmissionUpdate', updateCallback);

            this.connection.start()
                .then(() => this.watchSubmission(submittionId));
        }

        watchSubmission(submissionId) {
            console.log('Invoking watch for submission ' + submissionId);

            this.connection.invoke('WatchSubmission', submissionId);
        }
    }

    Vue.component('sitemap-inprogress', {
        template: '#sitemap-inprogress',
        props: {
            url: {
                type: String,
                required: true
            },
            results: {
                type: Array,
                required: true
            }
        },
        data: function () {
            const steps = Object.freeze({
                'parsing': 1,
                'validating': 2
            });

            return {
                steps: steps
            };
        },
        computed: {
            parsingStepClass() {
                return this.internalStep === this.steps.parsing
                    ? 'active-step'
                    : 'completed-step'; 
            },
            validatingStepClass() {
                return this.internalStep === this.steps.validating
                    ? 'active-step'
                    : '';
            },
            internalStep() {
                if (this.results.length === 0) {
                    return this.steps.parsing;
                }

                return this.results.length === 0 ? this.steps.parsing : this.steps.validating;
            }
        }
    });

    new Vue({
        el: '#sitemap-checker',
        data: {
            step: 'submit',
            submission: {
                url: ''
            },
            signalRConnection: null,
            results: [],
            steps: {
                inprogress: 'inprogress'
            },
            hub: new SitemapHub()
        },
        methods: {
            submitted(submission) {
                this.submission = submission;

                this.step = this.steps.inprogress;

                this.hub.watch(this.submission.id, this.submissionUpdate);
            },
            submissionUpdate(url, statusCode) {
                this.results.unshift({
                    url: url,
                    statusCode: statusCode
                });
            }
        }
    });
}