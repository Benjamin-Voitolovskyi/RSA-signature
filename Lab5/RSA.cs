using System;

namespace Lab5
{
    class RSA
    {
        //не потрібні, оскільки решту змінних захардкодили:
        //private int p = 3;
        //private int q = 11;
        private readonly int n = 33;
        private readonly int e = 7;
        private readonly int d = 3;
        private readonly string alphabet = "абвгдеєжзиіїйклмнопрстуфхцчшщьюя ";

        public int[] Encode(string text)
        {
            int[] encoded = new int[text.Length];

            for (int i = 0; i < text.Length; ++i)
            {
                int idx = Array.IndexOf(alphabet.ToCharArray(), text[i]);
                encoded[i] = (int)Mod((long)Math.Pow(idx, e), n);
            }

            return encoded;
        }

        public string Decode(int[] encoded)
        {
            string decoded = "";

            foreach (int ci in encoded)
            {
                int idx = (int)Mod((long)Math.Pow(ci, d), n);
                decoded += alphabet[idx];
            }

            return decoded;
        }

        private long Hash(string message)
        {
            int result = 0;

            for (int i = 0; i < message.Length; ++i)
                result += Array.IndexOf(alphabet.ToCharArray(), message[i]);

            return Mod(result, n);
        }

        private long Mod(long dividend, long divisor)
        {
            return (divisor + (dividend % divisor)) % divisor;
        }

        public long EncodeSign(string sign)
        {
            return Mod((long)Math.Pow(Hash(sign), e), n);
        }

        public bool CheckSign(string sign, int encodedSign)
        {
            return Mod((long)Math.Pow(encodedSign, d), n) == Hash(sign);
        }
    }
}
