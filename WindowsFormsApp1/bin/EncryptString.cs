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
            binaryList = DataManipulation.convertBitListToBinaryList(bitList, byteSize);
        }
    }
}
