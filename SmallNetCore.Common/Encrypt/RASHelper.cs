using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmallNetCore.Common.Encrypt
{
    public class RASHelper
    {
        #region RSA 的密钥产生

        /// <summary>RSA 的密钥产生 产生私钥 和公钥 </summary>
        /// <param name="xmlKeys"></param>
        /// <param name="xmlPublicKey"></param>
        public void RSAKey(out string xmlKeys, out string xmlPublicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            xmlKeys = rsa.ToXmlString(true);
            xmlPublicKey = rsa.ToXmlString(false);
        }

        #endregion RSA 的密钥产生

        #region RSA的加密函数

        /// <summary>RSA的加密函数  string</summary>
        /// <param name="xmlPublicKey"></param>
        /// <param name="m_strEncryptString"></param>
        /// <remarks>KEY必须是XML的行式,返回的是字符串 </remarks>
        /// <remarks>该加密方式有 长度 限制的！！</remarks>
        /// <returns></returns>
        public string RSAEncrypt(string xmlPublicKey, string m_strEncryptString)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            var plainTextBArray = (new UnicodeEncoding()).GetBytes(m_strEncryptString);
            var cypherTextBArray = rsa.Encrypt(plainTextBArray, false);
            var result = Convert.ToBase64String(cypherTextBArray);
            return result;
        }

        /// <summary>RSA的加密函数 byte[]</summary>
        /// <param name="xmlPublicKey"></param>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public string RSAEncrypt(string xmlPublicKey, byte[] encryptString)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            var cypherTextBArray = rsa.Encrypt(encryptString, false);
            var result = Convert.ToBase64String(cypherTextBArray);
            return result;
        }

        #endregion RSA的加密函数

        #region RSA的解密函数

        /// <summary>RSA的解密函数  string</summary>
        /// <param name="xmlPrivateKey"></param>
        /// <param name="m_strDecryptString"></param>
        /// <returns></returns>
        public string RSADecrypt(string xmlPrivateKey, string m_strDecryptString)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            var plainTextBArray = Convert.FromBase64String(m_strDecryptString);
            var dypherTextBArray = rsa.Decrypt(plainTextBArray, false);
            var result = (Encoding.ASCII).GetString(dypherTextBArray);
            return result;
        }

        /// <summary>RSA的解密函数  byte</summary>
        /// <param name="xmlPrivateKey"></param>
        /// <param name="decryptString"></param>
        /// <returns></returns>
        public string RSADecrypt(string xmlPrivateKey, byte[] decryptString)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            var dypherTextBArray = rsa.Decrypt(decryptString, false);
            var result = (Encoding.ASCII).GetString(dypherTextBArray);
            return result;
        }

        #endregion RSA的解密函数

        #region RSA数字签名

        #region 获取Hash描述表

        //获取Hash描述表
        public bool GetHash(string m_strSource, ref byte[] HashData)
        {
            //从字符串中取得Hash描述
            byte[] Buffer;
            HashAlgorithm MD5 = HashAlgorithm.Create("MD5");
            Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(m_strSource);
            HashData = MD5.ComputeHash(Buffer);
            return true;
        }

        //获取Hash描述表
        public bool GetHash(string m_strSource, ref string strHashData)
        {
            //从字符串中取得Hash描述
            byte[] Buffer;
            byte[] HashData;
            HashAlgorithm MD5 = HashAlgorithm.Create("MD5");
            Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(m_strSource);
            HashData = MD5.ComputeHash(Buffer);

            strHashData = Convert.ToBase64String(HashData);
            return true;
        }

        //获取Hash描述表
        public bool GetHash(System.IO.FileStream objFile, ref byte[] HashData)
        {
            //从文件中取得Hash描述
            HashAlgorithm md5 = HashAlgorithm.Create("MD5");
            HashData = md5.ComputeHash(objFile);
            objFile.Close();
            return true;
        }

        //获取Hash描述表
        public bool GetHash(System.IO.FileStream objFile, ref string strHashData)
        {
            //从文件中取得Hash描述
            byte[] HashData;
            HashAlgorithm MD5 = HashAlgorithm.Create("MD5");
            HashData = MD5.ComputeHash(objFile);
            objFile.Close();
            strHashData = Convert.ToBase64String(HashData);
            return true;
        }

        #endregion 获取Hash描述表

        #region RSA签名

        //RSA签名
        public bool SignatureFormatter(string p_strKeyPrivate, byte[] HashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(p_strKeyPrivate);
            RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            //设置签名的算法为MD5
            rsaFormatter.SetHashAlgorithm("MD5");
            //执行签名
            EncryptedSignatureData = rsaFormatter.CreateSignature(HashbyteSignature);
            return true;
        }

        //RSA签名
        public bool SignatureFormatter(string p_strKeyPrivate, byte[] HashbyteSignature, ref string m_strEncryptedSignatureData)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(p_strKeyPrivate);
            RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            //设置签名的算法为MD5
            rsaFormatter.SetHashAlgorithm("MD5");
            //执行签名
            var encryptedSignatureData = rsaFormatter.CreateSignature(HashbyteSignature);
            m_strEncryptedSignatureData = Convert.ToBase64String(encryptedSignatureData);
            return true;
        }

        //RSA签名
        public bool SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            var hashbyteSignature = Convert.FromBase64String(m_strHashbyteSignature);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(p_strKeyPrivate);
            RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            //设置签名的算法为MD5
            rsaFormatter.SetHashAlgorithm("MD5");
            //执行签名
            EncryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);
            return true;
        }

        //RSA签名
        public bool SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature, ref string m_strEncryptedSignatureData)
        {
            var hashbyteSignature = Convert.FromBase64String(m_strHashbyteSignature);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(p_strKeyPrivate);
            RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            //设置签名的算法为MD5
            rsaFormatter.SetHashAlgorithm("MD5");
            //执行签名
            var encryptedSignatureData = rsaFormatter.CreateSignature(hashbyteSignature);
            m_strEncryptedSignatureData = Convert.ToBase64String(encryptedSignatureData);
            return true;
        }

        #endregion RSA签名

        #region RSA 签名验证

        public bool SignatureDeformatter(string p_strKeyPublic, byte[] hashbyteDeformatter, byte[] DeformatterData)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(p_strKeyPublic);
            RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            //指定解密的时候HASH算法为MD5
            rsaDeformatter.SetHashAlgorithm("MD5");
            return rsaDeformatter.VerifySignature(hashbyteDeformatter, DeformatterData);
        }

        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, byte[] DeformatterData)
        {
            var hashbyteDeformatter = Convert.FromBase64String(p_strHashbyteDeformatter);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(p_strKeyPublic);
            RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            //指定解密的时候HASH算法为MD5
            rsaDeformatter.SetHashAlgorithm("MD5");
            return rsaDeformatter.VerifySignature(hashbyteDeformatter, DeformatterData);
        }

        public bool SignatureDeformatter(string p_strKeyPublic, byte[] hashbyteDeformatter, string p_strDeformatterData)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(p_strKeyPublic);
            RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            //指定解密的时候HASH算法为MD5
            rsaDeformatter.SetHashAlgorithm("MD5");
            var deformatterData = Convert.FromBase64String(p_strDeformatterData);
            return rsaDeformatter.VerifySignature(hashbyteDeformatter, deformatterData);
        }

        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, string p_strDeformatterData)
        {
            byte[] DeformatterData;
            byte[] HashbyteDeformatter;
            HashbyteDeformatter = Convert.FromBase64String(p_strHashbyteDeformatter);
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSA.FromXmlString(p_strKeyPublic);
            RSAPKCS1SignatureDeformatter RSADeformatter = new RSAPKCS1SignatureDeformatter(RSA);
            //指定解密的时候HASH算法为MD5
            RSADeformatter.SetHashAlgorithm("MD5");
            DeformatterData = Convert.FromBase64String(p_strDeformatterData);
            return RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData);
        }

        #endregion RSA 签名验证

        #endregion RSA数字签名
    }
}
