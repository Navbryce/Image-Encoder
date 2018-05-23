The tool lets one encode and decode encrypted messages in messages. It was first prototyped in Python and then coded with a GUI using C# and .NET. The Windows app allows one to easily embed messages as well as decrypt them and save the output.

The actual message encryption revolves around XOR. Messages are embedded in random LSB.

## Installation/Demo
**For Python**:
- $pipenv install
- Run either user-decode.py or user-encode.py depending on use case in /scripts

**For Windows App (C#) with install**:
- Run the setup.exe, and it will install the app completely

**For Windows App (C#) wihtout install**:
- Run the .exe in "\ImageEncoderDecoder\bin\Debug"
