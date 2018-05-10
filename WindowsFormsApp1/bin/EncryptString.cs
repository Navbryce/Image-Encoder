using System;
using System.Collections.Generic;
using WindowsFormsApp1.bin.Utilities;

namespace WindowsFormsApp1.bin {

    public class EncryptString
    {
        // Static variables
        public static int byteSize = 8;

        // Public variables
        public LinkedList<String> BinaryList
        {
            get
            {
                return binaryList;
            }
            set
            {
                binaryList = value;
            }
        }
        public LinkedList<char> BitList
        {
            get
            {
                return BitList;
            }
            set
            {
                BitList = value;
            }
        }
        public LinkedList<Byte> Bytes
        {
            get
            {
                return bytes;
            }
            set
            {
                bytes = value;
            }
        }
        public String Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }

        // private variables
        private LinkedList<String> binaryList;
        private LinkedList<char> bitList;
        private LinkedList<Byte> bytes;
        private String message;
        public EncryptString(String messageValue)
        {
            message = messageValue;
            bytes = DataManipulation.convertStringToBytes(message);
            binaryList = DataManipulation.convertByteListToBinaryList(bytes, byteSize);
            bitList = DataManipulation.convertBinaryListToBitList(binaryList);
        }
        public EncryptString(LinkedList<char> encryptedBits, String encryptString)
        {
            bitList = encryptedBits; // sets the private variable bitList to the encrypted bits
            decrypt(encryptString); // tries to decrypt. will update EVERy data structure except for the string
            message = null;
        }
        /// <summary>
        /// Encryption/DECRPYTION methods
        /// </summary>
        /// 
        public LinkedList<char> encrypt(String encryptKey)
        {
            LinkedList<char> comparisonBits = encryptEncryptionKey(encryptKey);
            LinkedList<char> encryptedMessageBits = encrypt(comparisonBits); // encrypt also will udpate the data structures (except the string)
            return encryptedMessageBits;
        }

        public LinkedList<char> encrypt(LinkedList<char> comparisonBits)
        {
            bitList = DataManipulation.bitXORList(bitList, comparisonBits);
            recreateDataStructuresFromBits();
            return bitList; 
        }

        /// <summary>
        /// Encrypts the "encrypt key" to add a little variation to the bits. Compares every bit in the encryption key to 1
        /// </summary>
        /// <param name="encryptKey">The encryption key</param>
        /// <returns></returns>
        public LinkedList<char> encryptEncryptionKey (String encryptKey)
        {
            EncryptString encryptKeyObject = new EncryptString(encryptKey);
            LinkedList<char> encryptKeyCompareBits = new LinkedList<char>(); // used to encrypt the encrypt key
            encryptKeyCompareBits.AddLast('1');
            LinkedList<char> encryptKeyBits = encryptKeyObject.encrypt(encryptKeyCompareBits); // encrypt the encryptKey with just '1' to a add a little variation
            return encryptKeyBits;
        }

        public LinkedList<char> decrypt(String encryptKey)
        {
            LinkedList<char> originalComparisonBits = encryptEncryptionKey(encryptKey);
            LinkedList<char> decryptedBits = decrypt(originalComparisonBits); // updates every data structure except for the original string
            return decryptedBits;
        }
        
        /// <summary>
        /// Recreates every data structure based on the decrypted bits EXCEPT for the original string value
        /// </summary>
        /// <param name="originalComparisonBits"></param>
        /// <returns></returns>
        public LinkedList<char> decrypt(LinkedList<char> originalComparisonBits)
        {
           LinkedList<char> decryptedBits = DataManipulation.bitXORListReverse(bitList, originalComparisonBits);
            bitList = decryptedBits;
            recreateDataStructuresFromBits();
            return decryptedBits;
        }



        /// <summary>
        /// Data manipulation methods
        /// </summary>
        /*
         * Assumes bitList has been changed in some way, so recreate everything else, EXCEPT for the string from the singleBits
         * */
        public void recreateDataStructuresFromBits()
        {
            binaryList = DataManipulation.convertBitListToBinaryList(bitList, byteSize);
            bytes = DataManipulation.convertBinaryListToByteList(BinaryList, byteSize);
        }

        /**
         * Will throw an exception if the bytes don't convert to an actual string
         * */
        public String recreateStringFromBytes() {
            try
            {
                String stringValue = DataManipulation.convertBytesToString(bytes);
                message = stringValue;
            } catch (Exception ex)
            {
                throw ex; // throw the error if an error occurred while converting the bytes to a string, probably because one of the bytes did not represent a real character
            }
            return message;
        }

        
    }
}
