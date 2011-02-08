namespace DotCopter.Hardware.Implementations.GHIElectronics.FEZPanda
{
    public static class FezPin
    {
        #region AnalogIn enum

        public enum AnalogIn : byte
        {
            An0,
            An1,
            An2,
            An3,
            An4,
            An5,
        }

        #endregion

        #region AnalogOut enum

        public enum AnalogOut : byte
        {
            An3,
        }

        #endregion

        #region Digital enum

        public enum Digital : byte
        {
            LDR = 0,
            UEXT6 = 1,
            Di5 = 2,
            UEXT4 = 3,
            IO4 = 4,
            UEXT3 = 5,
            UEXT8 = 6,
            Di6 = 7,
            UEXT7 = 8,
            UEXT9 = 10,
            UEXT5 = 11,
            UEXT10 = 12,
            IO13 = 13,
            IO14 = 14,
            Di7 = 15,
            IO16 = 16,
            IO17 = 17,
            Di1 = 18,
            Di4 = 19,
            Di0 = 20,
            IO21 = 21,
            An3 = 22,
            Di8 = 23,
            An2 = 24,
            Di9 = 25,
            An1 = 26,
            An0 = 28,
            An4 = 29,
            Di3 = 31,
            An5 = 32,
            Di2 = 33,
            Di10 = 35,
            IO38 = 38,
            IO39 = 39,
            Di12 = 40,
            Di11 = 41,
            Di13 = 42,
            LED = 43,
            IO44 = 44,
            IO45 = 45,
            IO46 = 46,
            IO47 = 47,
            IO48 = 48,
            IO49 = 49,
            IO50 = 50,
            IO51 = 51,
            IO52 = 52,
            IO53 = 53,
            IO54 = 54,
            IO55 = 55,
            IO56 = 56,
            IO57 = 57,
            IO58 = 58,
            IO59 = 59,
            IO60 = 60,
            IO61 = 61,
            IO62 = 62,
            IO63 = 63,
            IO64 = 64,
            IO65 = 65,
            IO66 = 66,
            IO67 = 67,
            IO68 = 68,
            IO69 = 69,
        }

        #endregion

        #region Interrupt enum

        public enum Interrupt : byte
        {
            LDR = 0,
            UEXT6 = 1,
            Di5 = 2,
            UEXT4 = 3,
            IO4 = 4,
            UEXT3 = 5,
            UEXT8 = 6,
            Di6 = 7,
            UEXT7 = 8,
            UEXT9 = 10,
            UEXT5 = 11,
            UEXT10 = 12,
            IO14 = 14,
            Di7 = 15,
            IO16 = 16,
            Di1 = 18,
            Di4 = 19,
            Di0 = 20,
            An3 = 22,
            An2 = 24,
            An1 = 26,
            An0 = 28,
            Di3 = 31,
            Di2 = 33,
            IO38 = 38,
            IO39 = 39,
            Di12 = 40,
            Di11 = 41,
            Di13 = 42,
            LED = 43,
            IO44 = 44,
            IO45 = 45,
            IO46 = 46,
            IO47 = 47,
            IO48 = 48,
            IO49 = 49,
            IO50 = 50,
        }

        #endregion

        #region PWM enum

        public enum PWM : byte
        {
            Di10 = 1,
            Di9 = 2,
            Di8 = 3,
            IO4 = 4,
            Di5 = 5,
            Di6 = 6,
        }

        #endregion
    }
}

