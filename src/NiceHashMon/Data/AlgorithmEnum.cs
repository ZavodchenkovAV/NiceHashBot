using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceHashMon.Data
{
    public enum AlgorithmEnum
    {
        Scrypt,
        SHA256,
        ScryptNf,
        X11,
        X13,
        Keccak,
        X15,
        Nist5,
        NeoScrypt,
        Lyra2RE,
        WhirlpoolX,
        Qubit,
        Quark,
        Axiom,
        Lyra2REv2,
        ScryptJaneNf16,
        Blake256r8,
        Blake256r14,
        Blake256r8vnl,
        Hodl,
        DaggerHashimoto,
        Decred,
        CryptoNight,
        Lbry,
        Equihash,
        Pascal,
        X11Gost,
        Sia,
        Blake2s,
        Skunk,
        CryptoNightV7
    }

    public static class AlgorithmEnumExt
    {
        public static long GetRateCoeff (this AlgorithmEnum algorithmEnum)
        {
            switch(algorithmEnum)
            {
                case AlgorithmEnum.SHA256:
                    return 1000000000000000;
                case AlgorithmEnum.Axiom:
                case AlgorithmEnum.Hodl:
                    return 1000;
                case AlgorithmEnum.ScryptJaneNf16:
                case AlgorithmEnum.CryptoNight:
                case AlgorithmEnum.Equihash:
                    return 1000000;
                case AlgorithmEnum.ScryptNf:
                case AlgorithmEnum.X13:
                case AlgorithmEnum.X15:
                case AlgorithmEnum.Nist5:
                case AlgorithmEnum.NeoScrypt:
                case AlgorithmEnum.Lyra2RE:
                case AlgorithmEnum.WhirlpoolX:
                case AlgorithmEnum.DaggerHashimoto:
                case AlgorithmEnum.X11Gost:
                case AlgorithmEnum.Skunk:
                    return 1000000000;
                default:
                    return 1000000000000;
            }
        }
    }
}
