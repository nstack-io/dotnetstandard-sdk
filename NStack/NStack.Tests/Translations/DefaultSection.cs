﻿using NStack.Extensions;
using NStack.Models;

namespace NStack.Tests.Translations
{
    public class DefaultSection : ResourceInnerItem
    {
        public DefaultSection() : base() { }
        public DefaultSection(ResourceInnerItem item) : base(item) { }

        public string Text => this[nameof(Text).FirstCharToLower()];
        public string Html => this[nameof(Html).FirstCharToLower()];
        public string BoolValue => this[nameof(BoolValue).FirstCharToLower()];
        public string BoolValueFalse => this[nameof(BoolValueFalse).FirstCharToLower()];
    }
}
