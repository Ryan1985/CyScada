/*!
 * jQuery ClassyLED
 * vox.SPACE
 *
 * Written by Marius Stanciu - Sergiu <marius@vox.space>
 * Licensed under the MIT license https://vox.SPACE/LICENSE-MIT
 * Version 1.3.2
 */
(function($) {
    $.fn.ClassyLED = function(settings) {
        var ClassyLED = function(element, settings) {
            this.type;
            this.format;
            this.color;
            this.backgroundColor;
            this.rounded;
            this.spacing;
            this.value;
            this.hourFormat;
            this.digits = 8;
            this.size;
            this.new_date;
            this.flash = true;
            this.dig = [];
            this.font1 = [" 000    0    000   000     0  00000   00  00000  000   000             ",
                "0   0  00   0   0 0   0   00  0      0        0 0   0 0   0            ",
                "0   0   0       0     0  0 0  0     0         0 0   0 0   0   0        ",
                "0   0   0    000   000  0  0  0000  0000     0   000   0000            ",
                "0   0   0   0         0 00000     0 0   0   0   0   0     0            ",
                "0   0   0   0     0   0    0  0   0 0   0  0    0   0    0    0        ",
                " 000   000  00000  000     0   000   000  0      000   00              "
            ];
            this.font2 = [" 000    0    000  00000    0  00000   00  00000  000   000             ",
                "0   0  00   0   0    0    00  0      0        0 0   0 0   0            ",
                "0  00   0       0   0    0 0  0000  0        0  0   0 0   0   0        ",
                "0 0 0   0     00     0  0  0      0 0000    0    000   0000            ",
                "00  0   0    0        0 00000     0 0   0   0   0   0     0            ",
                "0   0   0   0     0   0    0  0   0 0   0   0   0   0    0    0        ",
                " 000   000  00000  000     0   000   000    0    000   00              "
            ];
            this.font3 = ["00000     0 00000 00000 0   0 00000 00000 00000 00000 00000            ",
                "0   0     0     0     0 0   0 0     0         0 0   0 0   0   0        ",
                "0   0     0     0     0 0   0 0     0         0 0   0 0   0            ",
                "0   0     0 00000 00000 00000 00000 00000     0 00000 00000            ",
                "0   0     0 0         0     0     0 0   0     0 0   0     0            ",
                "0   0     0 0         0     0     0 0   0     0 0   0     0   0        ",
                "00000     0 00000 00000     0 00000 00000     0 00000 00000            "
            ];
            this.led;
            this.__constructor = function(conf) {
                var start = new Date();
                this.type = typeof conf.type !== 'undefined' ? conf.type : 'time';
                this.value = typeof conf.value !== 'undefined' ? conf.value : null;
                if (this.value !== null) {
                    this.digits = this.value.toString().length;
                }
                this.format = typeof conf.format !== 'undefined' ? conf.format : 'hh:mm';
                this.color = typeof conf.color !== 'undefined' ? conf.color : "#FFF";
                this.backgroundColor = typeof conf.backgroundColor !== 'undefined' ? conf.backgroundColor : "#000";
                this.rounded = typeof conf.rounded !== 'undefined' ? conf.rounded : 1;
                this.spacing = typeof conf.spacing !== 'undefined' ? conf.spacing : 1;
                this.hourFormat = typeof conf.hourFormat !== 'undefined' ? conf.hourFormat : 24;
                this.fontType = typeof conf.fontType !== 'undefined' ? conf.fontType : 1;
                this.led = this['font' + this.fontType];
                this.size = typeof conf.size !== 'undefined' ? conf.size : 12;
                this.countTo = typeof conf.countTo !== 'undefined' ? conf.countTo : start.getFullYear() + 1;
                var h_w = 12, r, self = this;
                if (this.size < 30) {
                    h_w = this.size;
                }
                function mtimer(timer) {
                    var n_t = timer.split(":");
                    for (var i = 0; i < n_t.length; i++) {
                        n_t[i] = parseInt(n_t[i], 10);
                    }
                    return n_t;
                }
                function updateTime() {
                    var d = new Date();
                    if (self.type === "countdown") {
                        self.updateLed1(d);
                    }
                    if (self.type === "time") {
                        self.updateLed2(d);
                    }
                    if (self.type === "random" || self.type === "number") {
                        self.updateLed0();
                    }
                    else {
                        setTimeout(updateTime, 1000);
                    }
                }
                if (this.type === "random") {
                    r = Raphael($(element)[0], this.digits * 6 * (h_w + this.spacing) - (h_w + 2 * this.spacing), 7 * (h_w + this.spacing) - this.spacing);
                    for (var i = 0; i < this.digits * 6; i++) {
                        this.dig[i] = [];
                        for (var y = 0; y < 7; y++) {
                            this.dig[i][y] = r.rect(i * (h_w + this.spacing), y * (h_w + this.spacing), h_w, h_w, this.rounded).attr({
                                fill: this.backgroundColor,
                                stroke: null
                            });
                        }
                    }
                    updateTime();
                }
                else if (this.type === "number") {
                    r = Raphael($(element)[0], this.digits * 6 * (h_w + this.spacing) - (h_w + 2 * this.spacing), 7 * (h_w + this.spacing) - this.spacing);
                    for (var i = 0; i < this.digits * 6; i++) {
                        this.dig[i] = [];
                        for (var y = 0; y < 7; y++) {
                            this.dig[i][y] = r.rect(i * (h_w + this.spacing), y * (h_w + this.spacing), h_w, h_w, this.rounded).attr({
                                fill: this.backgroundColor,
                                stroke: null
                            });
                        }
                    }
                    updateTime();
                }
                else {
                    r = Raphael($(element)[0], this.format.length * 6 * (h_w + this.spacing) - (h_w + 2 * this.spacing), 7 * (h_w + this.spacing) - this.spacing);
                }
                if (this.type === "countdown") {
                    var n_t = mtimer(this.countTo + ":1:1:0:0:00");
                    this.new_date = new Date(n_t[0], n_t[1] - 1, n_t[2], n_t[3], n_t[4]);
                    for (var i = 0; i < 12 * 6; i++) {
                        this.dig[i] = [];
                        for (var y = 0; y < 7; y++) {
                            this.dig[i][y] = r.rect(i * (h_w + this.spacing), y * (h_w + this.spacing), h_w, h_w, this.rounded).attr({
                                fill: this.backgroundColor,
                                stroke: null
                            });
                        }
                    }
                    updateTime();
                }
                if (this.type === "time") {
                    for (var i = 0; i < ((this.format === "mm:ss" || this.format === "hh:mm") ? 5 : ((this.format === "hh" || this.format === "mm" || this.format === "ss") ? 2 : 8)) * 6; i++) {
                        this.dig[i] = [];
                        for (var y = 0; y < 7; y++) {
                            this.dig[i][y] = r.rect(i * (h_w + this.spacing), y * (h_w + this.spacing), h_w, h_w, this.rounded).attr({
                                fill: this.backgroundColor,
                                stroke: null
                            });
                        }
                    }
                    updateTime();
                }
            };
            this.updateLed0 = function() {
                var num = '';
                if (this.type === 'random') {
                    var rand_num = ("0,9999999").split(",");
                    num = "" + parseInt(parseInt(rand_num[0]) + Math.random() * (parseInt(rand_num[1]) - parseInt(rand_num[0])));
                    while (String(num).length < this.digits) {
                        num = "0" + num;
                    }
                }
                else {
                    num = this.value.toString();
                }
                this._tick(num);
            };
            this.updateLed1 = function(d) {
                var time_rem = parseInt((this.new_date.getTime() - d.getTime()) / 1000) + 1;
                var m_d = parseInt(time_rem / 86400);
                var m_h = parseInt((time_rem - m_d * 86400) / 3600);
                var m_m = parseInt((time_rem - m_d * 86400 - m_h * 3600) / 60);
                var m_s = parseInt(time_rem - m_d * 86400 - m_h * 3600 - m_m * 60);
                var num = (m_d < 10 ? "00" : (m_d > 100 ? "" : "0")) + m_d + ":" + (m_h < 10 ? "0" : "") + m_h + ":" + (m_m < 10 ? "0" : "") + m_m + ":" + (m_s < 10 ? "0" : "") + m_s;
                if ((this.new_date.getTime() - d.getTime()) >= 0) {
                    this._tick(num);
                }
                else {
                    this._tick("000:00:00:00");
                }
            };
            this.updateLed2 = function(d) {
                var num;
                var m_h = parseInt(d.getHours());
                var m_h = (this.hourFormat === 12 ? (m_h > 12 ? m_h - 12 : m_h) : m_h);
                var m_m = parseInt(d.getMinutes());
                var m_s = parseInt(d.getSeconds());
                if (this.format === "mm:ss") {
                    num = (m_m < 10 ? "0" : "") + m_m + ":" + (m_s < 10 ? "0" : "") + m_s;
                }
                else if (this.format === "hh:mm") {
                    if (this.flash) {
                        num = (m_h < 10 ? "0" : "") + m_h + ":" + (m_m < 10 ? "0" : "") + m_m;
                    }
                    else {
                        num = (m_h < 10 ? "0" : "") + m_h + " " + (m_m < 10 ? "0" : "") + m_m;
                    }
                    this.flash = !this.flash;
                }
                else if (this.format === "hh") {
                    num = (m_h < 10 ? "0" : "") + m_h;
                }
                else if (this.format === "mm") {
                    num = (m_m < 10 ? "0" : "") + m_m;
                }
                else if (this.format === "ss") {
                    num = (m_s < 10 ? "0" : "") + m_s;
                }
                else {
                    num = (m_h < 10 ? "0" : "") + m_h + ":" + (m_m < 10 ? "0" : "") + m_m + ":" + (m_s < 10 ? "0" : "") + m_s;
                }
                this._tick(num);
            };
            this._tick = function(num) {
                var razd = 0;
                for (var l = 0; l < num.length; l++) {
                    num.charAt(l) === ":" ? razd = 10 : (num.charAt(l) === " " ? razd = 11 : razd = num.charAt(l));
                    for (var i = 0; i < 6; i++) {
                        for (var y = 0; y < 7; y++) {
                            if (this.led[y].charAt(razd * 6 + i) === "0" && this.dig[l * 6 + i][y].attrs.fill === this.backgroundColor) {
                                this.dig[l * 6 + i][y].animate({
                                    fill: this.color
                                }, 300);
                            }
                            else if (this.led[y].charAt(razd * 6 + i) === " " && this.dig[l * 6 + i][y].attrs.fill !== this.backgroundColor) {
                                this.dig[l * 6 + i][y].animate({
                                    fill: this.backgroundColor
                                }, 300);
                            }
                        }
                    }
                }
            };
            return this.__constructor(settings);
        };
        return this.each(function() {
            settings.id = $(this).attr('id');
            return new ClassyLED(this, settings);
        });
    };
})(jQuery);