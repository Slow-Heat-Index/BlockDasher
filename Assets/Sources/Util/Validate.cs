using System;

namespace Sources.Util {
    public static class Validate {
        public static void ValidateNotNull(this object o, string message = null) {
            if (o == null) throw new ArgumentNullException(nameof(o), message);
        }

        public static void ValidateTrue(this bool b, string message = "Expression is false.") {
            if (!b) throw new ArgumentException(message);
        }

        public static void ValidateFalse(this bool b, string message = "Expression is true.") {
            if (b) throw new ArgumentException(message);
        }
    }
}