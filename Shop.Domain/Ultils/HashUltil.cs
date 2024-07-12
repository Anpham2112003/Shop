﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Ultils
{
    public static class HashUltil
    {
        public static string GenerateHash(string data)
        {
            var byteCode = Encoding.UTF8.GetBytes(data);

            var hash = SHA256.HashData(byteCode);

            return Convert.ToHexString(hash);
        }

        public static bool VerifyHash(string hash, string originalValue)
        {
            var byteCode = Convert.FromHexString(hash);

            var originalByteCode = Encoding.UTF8.GetBytes(originalValue);

            var originalHash = SHA256.HashData(originalByteCode);

            var compare = byteCode.SequenceEqual(originalHash);

            return compare;
        }
    }
}
