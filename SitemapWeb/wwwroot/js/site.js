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
                }).then(() => {
                    self.submitting = false;

                    self.$emit('submitted', {
                        id: '12345',
                        url: self.url
                    });
                }).catch(() => {
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
            }
        },
        methods: {
            submitted: function (submission) {
                this.submission = submission;

                this.step = 'inprogress';
            }
        }
    });
}