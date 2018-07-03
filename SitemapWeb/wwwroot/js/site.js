// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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
            return {
                
            };
        }
    });

    new Vue({
        el: '#sitemap-checker',
        data: {
            step: 'submit',
            submission: {
                url: ''
            },
            results: []
        },
        methods: {
            submitted: function (submission) {
                this.submission = submission;

                this.step = 'inprogress';

                const connection = new signalR.HubConnectionBuilder()
                    .withUrl('/sitemaphub')
                    .build();

                connection.on('SubmissionUpdate', (url, statusCode) => {
                    this.results.unshift({
                        url: url,
                        statusCode: statusCode
                    });
                });

                connection.start().then(() => {
                    connection.invoke('WatchSubmission', submission.id);

                    console.log('Invoking watch for submission ' + submission.id);
                });
            }
        }
    });
}