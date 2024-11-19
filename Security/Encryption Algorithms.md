**Symmetric Encryption Algorithms (Same key for encryption and decryption)**

| Name | Security | Applications |
|---|---|---|
| DES | Considered insecure, susceptible to brute force attacks | Once widely used in government and banking systems, now replaced by AES |
| AES | Currently very secure, modern encryption standard | Widely used for data encryption, VPNs, SSL/TLS, etc. |
| RC4 | Stream cipher, has known weaknesses, not recommended | Used in SSL/TLS and WEP encryption |
| Blowfish | Relatively secure, suitable for embedded devices and low-resource environments | Used as an alternative to DES |
| IDEA | More secure than DES, but slower encryption | Mainly used in PGP (Pretty Good Privacy) encryption tool |
| Twofish | More secure and flexible than Blowfish, AES candidate | Used in encryption software like TrueCrypt |
| Serpent | Designed for security, high strength, but computationally complex | Although highly secure, poor computational performance, not widely used |

**Asymmetric Encryption Algorithms (Different keys for encryption and decryption)**

| Name | Principle | Security | Applications |
|---|---|---|---|
| RSA | Based on the difficulty of factoring large numbers | Relatively high, widely used for digital signatures and encryption in digital certificates (like SSL/TLS) |
| ECC | Based on elliptic curve mathematics, provides the same security level as RSA with shorter keys | More efficient than RSA at the same security level | Widely used in modern encryption systems, such as mobile devices and IoT |
| DSA | Based on the discrete logarithm problem, used to generate digital signatures | Similar to RSA, but only used for signing, not encryption | Digital Signature Standard (DSS) and some Public Key Infrastructure (PKI) applications |
| ElGamal | Based on the discrete logarithm problem, supports both encryption and signing | Relatively secure, suitable for encryption and signing, but computationally complex | Used in some encryption protocols, like PGP |
| Diffie-Hellman | Key exchange protocol, allows two parties to securely exchange keys over an insecure channel | Does not provide direct encryption, but can be used to generate shared keys | Used in SSL/TLS, VPN protocols for key exchange |
| Paillier | A type of homomorphic encryption algorithm, allows certain types of operations on encrypted data | Mathematically based on large number problems, suitable for protecting privacy | Privacy computing and cryptocurrency fields |

**Hash Functions (For data integrity verification)**

| Name | Output | Security | Applications |
|---|---|---|---|
| MD5 | 128-bit hash value | Has been proven to be prone to collisions, no longer secure | Once used for digital signatures, file integrity checks, etc., but now not recommended |
| SHA-1 | 160-bit hash value | Also has collision vulnerabilities, being phased out | Once used in digital certificates, TLS/SSL, etc., but no longer recommended |
| SHA-256 | 256-bit/512-bit hash value | SHA-2 series is considered the most secure hash function currently, widely used | Widely used in digital signatures, TLS/SSL, blockchain, etc. |
| BLAKE2 | Variable length hash value | Faster and secure, strong collision resistance | Widely used in modern encryption systems, especially in file verification and data integrity fields |

