#### A. Encryption Approaches
###### 1. Encryption At Rest
(+) Encryption at rest refers to the protection of data stored on physical or digital storage media, such as hard drives, databases, or cloud storage. The goal is to secure the data in case someone gains unauthorized access to the storage medium.

###### 2. Encryption In Transit
(+) Encryption in transit refers to the protection of data as it travels over a network, ensuring that it cannot be intercepted or tampered with by unauthorized entities during transmission.

![[Pasted image 20240104101805.png]]

#### B. Encryption Concepts
###### 1. Symmetric Encryption
(+) Symmertric Encryption refers to the practice of using an `algorithm` to encrypt a `plaintext` using a `key`, and after that decipher the `ciphertext` using the same `key`. This creates problem as the key needs to be exchanged between receiving parties to decipher the text. 
	(-) Symmetric Encryption is a feasible method for local encryption but not so much for encryption of data in transit, where the key needs to be exchanged beforehand for time-sensitive data.

![[Pasted image 20240104102448.png | 600]]

###### 2. Asymmetric Encryption
(+) Asymmetric Encryption refers to the practice of creating two keys `public key` and `private key`. `Public key` is used to create ciphertext and `private key` is used to decipher the text. The `public key` is available for anyone to create ciphertext, but only the designated recipient has the `private key` to decipher it.

![[Pasted image 20240104102947.png | 600]]

`NOTE`
(+) In order to ensure that the message from parties using Asymmetric Encryption is the designated sender, we use a method called `signing`. This is done by the recipent using their `private key`. The recipient will sign the reply using their `private key` and send to the sender. When receiving, the sender will use the `public key` to verify that the reply message is from the designated receiver.

![[Pasted image 20240104103541.png | 600]]

###### 3. Steganography
(+) One limits of Encryption is that everybody knows when we encrypt something and we might be forced to decrypt the message, the solution could be using `steganography` meaning the practice of hiding something inside something else.

![[Pasted image 20240104103920.png | 600]]

#### D. Key Management Service (KMS)
(+) KMS is a regional and Public service that allows the creation, storage and management of `keys`, both `Symmertric` and `Asymetric` Keys. As well as Cryptographic operations `encrypt or decrypt and...`.
(+) Keys created on KMS never leaves KMS - provide `FIPS 140-2 Level 2` 

###### 1. Customer Master Keys (CMK)
(+) CMK is logical think of it as a container containing `ID, date, resource policy, desc & state`. 
(+) CMK can be used to `directly encryp or decrypt` data up to 4KB.

`The way KMS and CMK operates are as follows:`
(+) A user create a key on KMS using CreateKey operation, as long as they are given the permission to do so. KMS will create a CMK for that user and encrypt that key for persistent storage.
(+) A user will then might want to use that CMK for encrypting data (again as long as they are given permission to do so), KMS will then decrypt the encrypted CMK to encrypt the requested data and provide the user with encrypted data.
(+) A user will then might want to decipher the encrypted data (again as long as they are given permission to do so, as CreateKey, Encrypt, and Decrypt are different operation that required different permission), KMS will then decrypt the encrypted CMK to decipher the requested data and provide the user with the decrypted data.

![[Pasted image 20240104105447.png]]

(+) Note that at no point is the CMK (both encrypted and decrypted) leaves KMS, only the data that user provided is leaving from and to KMS.
(+) As CMK can only works up to 4KB of data, there needs to be other way to handle data above this point, the solution is `Data Encryption Key`

###### 2. Data Encryption Key (DEKs)
(+) The reason that CMK does not allow data above 4KB is because it does not need to, CMK is the master key that is used to create other key for other operation, one of them is `Data Encryption Key` using `GenerateDataKey` operation for encrypting data above 4KB.

(+) `NOTE THAT` KMS does not store the DEKs, KMS creates the keys and deliver to the user or to the service creating the keys. The way this works is as follows
	(1) KMS creates the key with two version `plaintext version` and `ciphertext version`. 
	(2) User would then use the `plaintext version` to encrypt some data, then we will `discard` the plaintext keys leaving us with the `encrypted data` and the `encrypted data encryption keys`.
	(3) When required, the user will provide KMS with the `encrypted data` and `encrypted data encryption key`, KMS will used the CMK that was used to create the DEKs to decrypt the data and send back to the user.

###### 3. Key Policies and Security
(+) Key policies is a resource policy that every CMK has one.
(+) Key policies need to be explicitly told to trust an IAM user, in the example below, the key policies is explicitly told that user 111122223333 is entrusted to use KMS with all of its resource. The user 111122223333 can then assign 

![[Pasted image 20240104111644.png]]

#### Example
`Using KMS in CLI to encrypt data`
![[Pasted image 20240104122539.png]]

`Using KMS in CLI to decrypt data`
![[Pasted image 20240104122610.png]]

