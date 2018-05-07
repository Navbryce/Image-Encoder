using System;
using System.Collections.Generic;
using WindowsFormsApp1.bin.Utilities;

namespace WindowsFormsApp1.bin {

    public class EncryptString
    {
        // Static variables
        public static int byteSize = 8;

        // Public variables
        public LinkedList<String> binaryList
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
        public LinkedList<Byte> bytes
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
        public String message
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
        public EncryptString(String messageValue)
        {
            message = messageValue;
            bytes = DataManipulation.convertStringToBytes(message);
            binaryList = DataManipulation.convertByteListToBinaryList(bytes, byteSize);
        }
    }
}
