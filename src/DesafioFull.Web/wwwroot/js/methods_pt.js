﻿/*
* Localized default methods for the jQuery validation plugin.
* Locale: PT_BR
*/
//jQuery.extend(jQuery.validator.methods, {
//    date: function (value, element) {
//        return this.optional(element) || /^\d\d?\/\d\d?\/\d\d\d?\d?$/.test(value);
//    },
//    number: function (value, element) {
//        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:\.\d{3})+)(?:,\d+)?$/.test(value);
//    }
//});

$.validator.methods.range = function (value, element, param) {
    var globalizedValue = value.replace(",", ".");
    return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
}

$.validator.methods.number = function (value, element) {
    return this.optional(element) || /-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
}
