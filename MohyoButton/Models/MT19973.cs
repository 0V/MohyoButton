using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohyoButton.Models
{
    /* 
       Copyright (C) 1997 - 2002, Makoto Matsumoto and Takuji Nishimura,
       All rights reserved.                          

       Redistribution and use in source and binary forms, with or without
       modification, are permitted provided that the following conditions
       are met:

         1. Redistributions of source code must retain the above copyright
            notice, this list of conditions and the following disclaimer.

         2. Redistributions in binary form must reproduce the above copyright
            notice, this list of conditions and the following disclaimer in the
            documentation and/or other materials provided with the distribution.

         3. The names of its contributors may not be used to endorse or promote 
            products derived from this software without specific prior written 
            permission.

       THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
       "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
       LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
       A PARTICULAR PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL THE COPYRIGHT OWNER OR
       CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
       EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
       PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
       PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
       LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
       NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
       SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
    */

    public class MT19937
    {
        private const int NumberN = 624;
        private const int NumberM = 397;
        private const uint MatrixA = 0x9908b0dfU;
        private const uint UpperMask = 0x80000000U;
        private const uint LowerMask = 0x7fffffffU;
        private uint[] mt = new uint[NumberN];
        private int mti = NumberN + 1;

        public MT19937() { }

        public MT19937(uint s)
        {
            InitGet(s);
        }

        public MT19937(uint[] initKey)
        {
            InitByArray(initKey);
        }

        public void InitGet(uint s)
        {

            mt[0] = s & 0xffffffffU;
            for (mti = 1; mti < NumberN; mti++)
            {
                mt[mti] = (1812433253U * (mt[mti - 1] ^ (mt[mti - 1] >> 30)) + (uint)mti);
                mt[mti] &= 0xffffffffU;
                /* for >32 bit machines */
            }
        }

        public void InitByArray(uint[] initKey)
        {
            int i, j, k;
            int keyLength = initKey.Count();
            InitGet(19650218U);
            i = 1; j = 0;
            k = (NumberN > keyLength ? NumberN : keyLength);
            for (; k > 0; k--)
            {
                mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1664525U)) + initKey[j] + (uint)j; /* non linear */
                mt[i] &= 0xffffffffU; /* for WORDSIZE > 32 machines */
                i++; j++;
                if (i >= NumberN) { mt[0] = mt[NumberN - 1]; i = 1; }
                if (j >= keyLength) j = 0;
            }
            for (k = NumberN - 1; k > 0; k--)
            {
                mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1566083941U)) - (uint)i; /* non linear */
                mt[i] &= 0xffffffffU; /* for WORDSIZE > 32 machines */
                i++;
                if (i >= NumberN) { mt[0] = mt[NumberN - 1]; i = 1; }
            }

            mt[0] = 0x80000000U; /* MSB is 1; assuring non-zero initial array */
        }

        public uint GetInt32()
        {

            uint y;
            uint[] mag01 = new uint[2] { 0x0U, MatrixA };

            if (mti >= NumberN)
            { /* generate N words at one time */
                int kk;

                if (mti == NumberN + 1)   /* if init_genrand() has not been called, */
                    InitGet(5489U); /* a default initial seed is used */

                for (kk = 0; kk < NumberN - NumberM; kk++)
                {
                    y = (mt[kk] & UpperMask) | (mt[kk + 1] & LowerMask);
                    mt[kk] = mt[kk + NumberM] ^ (y >> 1) ^ mag01[y & 0x1U];
                }
                for (; kk < NumberN - 1; kk++)
                {
                    y = (mt[kk] & UpperMask) | (mt[kk + 1] & LowerMask);
                    mt[kk] = mt[kk + (NumberM - NumberN)] ^ (y >> 1) ^ mag01[y & 0x1U];
                }
                y = (mt[NumberN - 1] & UpperMask) | (mt[0] & LowerMask);
                mt[NumberN - 1] = mt[NumberM - 1] ^ (y >> 1) ^ mag01[y & 0x1U];

                mti = 0;
            }

            y = mt[mti++];

            /* Tempering */
            y ^= (y >> 11);
            y ^= (y << 7) & 0x9d2c5680U;
            y ^= (y << 15) & 0xefc60000U;
            y ^= (y >> 18);

            return y;
        }

        /* generates a random number on [0,0x7fffffff]-interval */
        public uint GetInt31()
        {
            return (uint)(GetInt32() >> 1);
        }

        /* generates a random number on [0,1]-real-interval */
        public double GetReal1()
        {
            return GetInt32() * (1.0 / 4294967295.0);
            /* divided by 2^32-1 */
        }

        /* generates a random number on [0,1)-real-interval */
        public double GetReal2()
        {
            return GetInt32() * (1.0 / 4294967296.0);
            /* divided by 2^32 */
        }

        /* generates a random number on (0,1)-real-interval */
        public double GetReal3()
        {
            return (((double)GetInt32()) + 0.5) * (1.0 / 4294967296.0);
            /* divided by 2^32 */
        }

        /* generates a random number on [0,1) with 53-bit resolution*/
        public double GetReal53()
        {
            uint a = GetInt32() >> 5, b = GetInt32() >> 6;
            return (a * 67108864.0 + b) * (1.0 / 9007199254740992.0);
        }
        /* These real versions are due to Isaku Wada, 2002/01/09 added */


    }
}
