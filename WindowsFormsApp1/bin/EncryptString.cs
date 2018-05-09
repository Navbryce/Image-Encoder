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
