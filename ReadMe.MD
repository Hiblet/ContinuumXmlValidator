# ContinuumOAuth1

Alteryx C# plug-in to create the HTTP Authorization header required for OAuth 1.  The tool takes the various fields used in OAuth 1.0 and 1.0a, and applies the sorting and encoding required, and then generates a new field called "Authorization".  This field contains a string that should be used as the HTTP Authorization header in OAuth flow.

This tool is the copyright protected property of Continuum (www.continuum.je)



## Introduction

This project is a Windows Visual Studio 2017 project for .Net Framework 4.6.1.  It is a Class Library DLL for use with Alteryx, compiled to x64.

OAuth 1.0 and 1.0a is designed to allow messages to be passed over HTTP without being tampered with.  HTTPS (SSL) has now superceded this, and OAuth 2.0 supercedes OAuth 1.0, but OAuth 1.0 is still in use where HTTP (unencrypted) traffic is used.

The principle is that when two counterparties pass a messsage, the message includes an encrypted version of itself.  Both counterparties have the keys and secrets required to encrypt the message, and a standard process for the encryption steps.  For an uncorrupted message, the receiver should be able to run their encryption process and end up with the same value as that attached to the original message.  If a bad actor has altered the plaintext version of the message, the encryption will generate a different value, and the receiver will know that the message is not to be trusted.

OAuth has standard fields that must be supplied, and some optional fields.



## Getting Started

You will need Visual Studio 2017 (Community edition is free).  You will need Alteryx Designer.

Download the repo and compile the project using Visual Studio 2017.  You should end up with a DLL in your debug and release folders.  Contact me if you just want a copy of the DLL pre-compiled.

Assuming you are using the default installation folder, copy the DLL to "C:\Program Files\Alteryx\bin\Plugins".  Alteryx will discover the plug in tool automatically.  The tool will then be found using the Alteryx Search function, and you should search for "OAuth1".  The logo is based on the Developer tool series logo (grey cog wheel with a letter icon, with a black OAuth logo central).  

The tool expects you to map it to the various required and optional fields.  All fields should exist in the source data stream.  If a data value is optional, include the field, but pass an empty string as the value.

The tool expects two fields as input, an output field, and an algorithm to use for hashing.  The input fields are for the message to hash, and the secret string to use to hash it.  The output field is appended to each data record.  The algorithm is one of the C# HMAC algorithms:
 


## OAuth Fields



### HttpVerb [Required]

The HTTP Protocol has standard words to represent actions:

 - GET : Retrieve data
 - POST : Send new data
 - PUT : Change existing data
 - DELETE : Remove data

This tool will tolerate other extended verbs, but these are the main actions used in the RESTful protocol.

The system will capitalise this verb if it is provided in lower case.



### URL [Required]

The URL field should hold the address of the service that you are trying to use.  It should be of the form "http://www.example.com", in that it should include the protocol prefix and www prefix.  It should not include the query string.  

The query string is additional data a web page will often append to a URL, eg "http://www.example.com?name=Alice&age=27".  A question mark signals the start of the query string.



### Query [Optional]

The Query String (See above) is any additional data that you will be sending on this request.  This value may be blank.  This value may or may not include the starting question mark, it is effectively ignored.

If the query string can be passed in UrlEncoded format, or not.  The tool will decode the string if required.



### Payload [Optional]

The Payload field should hold any data that is sent as www-form-urlencoded key value pairs.  It should not include JSON data or binary data, or XML data.

Form encoding is very similar to the encoding used in the query string, with variables shown as key-value pairs.  For example, form encoded data may appear as "name=Alice&age=27".  The tool will handle both unencoded data, and UrlEncoded data.



### Consumer Key [Required]

The consumer key is the publicly visible key value that the Service provider (Twitter, Google, Facebook etc) give you when you sign up to use their API.  This field should hold the key unencoded.



### Consumer Secret [Required]

The consumer secret is the counterpart to the consumer key and it should be kept secret by the user of the service.  This field should hold the data unencoded.



### Token [Optional]

The token will be a string provided by the service provider.  When you make initial contact with the provider you will not have a token and this field should be blank.  Once a provider has supplied a token, it should be held in this field, unencoded.



### Token Secret [Optional]

The token secret is a string provided by the service provider, and should be kept secret by the user.  This field should hold data unencoded.



### Timestamp [Required/SystemDefaults]

This field holds the Unix Epoch Timestamp (seconds since 01-JAN-1970) as a string representation of an integer value.  This field prevents old stale messages from being re-processed.  The tool will provide a timestamp equivalent to 'now' if this field is left blank.  You can over-ride this field by entering your own timestamp if you choose, but this should only be done if you are trying to debug the system and replicate a particular error.  Therefore the recommendation is that this field is left empty.



### Nonce [Required/SystemDefaults]

This field holds a code that should only be used once.  Again, this field is designed to prevent accidental re-processing of the same message.  The tool will provide a unique code for each message if the field is left blank.  You may over-ride this field, but you should only do so if trying to replicate an error.  Therefore the recommendation is that this field is left empty.



### Signature Method [Required]

This field should hold one of the following values:

 - HMAC-SHA1
 - HMAC-SHA256
 - HMAC-SHA384
 - HMAC-SHA512
 - HMAC-MD5
 - HMAC-RIPEMD160
 
This field determines the encryption algorithm used to hash the message.  The service provider should clearly stipulate which method to use.  The vast majority of services will use HMAC-SHA1.  



### Version [Required]

This field should contain either "1.0" or "1.0a", either is good.  If you do not use "1.0a", the tool will default to using "1.0".  Therefore this field may be left blank, but you should be aware that blank implies 1.0



### Realm [Optional]

Realm is the security realm of the service provider.  It will most likely be the domain of the service provider, eg "www.example.com" .  For OAuth purposes this field may be left blank, but some service providers may require it.  The field is not used in the calculation of the hashed message, but it is included as a parameter in the final Authorization HTTP header value.



### Verifier [Optional]

In the OAuth process, the service provider may return a string that indicates they have permissioned a token.  If the string is populated, it is used in the OAuth hashing calculation, but if the OAuth process step does not require the string, it may be left empty.  



### Output Field [Default]

The final field in the tool control allows you to set the output field name.  This defaults to "Authorization".  The reason is that the Download Tool in Alteryx can then use this field as the Authorization header.  Only change the name of this field if you are not going to use the tool with the Download Data tool, and you know what you are doing.




## Installation

Once you get to the stage where you have a compiled DLL, you are almost there.  If you do not want to get there, I can upload a copy of the compiled DLL to you on request, or email it, or whatever you like.

The DLL is then copied to "C:\Program Files\Alteryx\bin\Plugins\ContinuumOAuth1" on your local installation.  

Next, you need an INI file in "C:\Program Files\Alteryx\Settings\AdditionalPlugIns".  The INI file is included in this repo, so you can copy it to your local folder.  The INI file has the path info that Alteryx needs to find the DLL, and the ToolGroup that the tool should appear in, in this case "Developer".

If you are not worried about putting the tool in a particular group, you can just copy the DLL to "C:\Program Files\Alteryx\bin\Plugins".  Once the DLL is there, it will become available in the Alteryx Designer Search tool, as "OAuth1".

The tool will have a grey-cog-wheel icon with a black OAuth logo, a capital A.



## Example

Below is an example input data record:

```
-------------------------------------------------------------------------------------------------------------------------------
|Verb|URL                |Query      |Payload|ConsKey|ConsSecret|Token|TokenSecret|Time|Nonce|SigMethod|Version|Realm|Verifier|
-------------------------------------------------------------------------------------------------------------------------------
|GET |https://www.abc.com|?name=Alice|       |FDE9...|X8YC...   |     |           |    |     |HMAC-SHA1|1.0a   |     |        |
-------------------------------------------------------------------------------------------------------------------------------
```

This would be an example of an initialising message where no token or tokensecret has yet been provided.
The service provider should supply a ConsumerKey and ConsumerSecret when the user signs up to use their API service.
The tool will add a Timestamp and Nonce, and all other fields may be left blank.

```
-----------------------------------------------------------------------------------
|Authorization                                                                    | 
-----------------------------------------------------------------------------------
|OAuth realm="www.abc.com", oauth_consumer_key="YAN...", oauth_nonce="wfmg93ai",  | 
| oauth_signature="4Mi...Y%3D", oauth_signature_method="HMAC-SHA1",               |
| oauth_timestamp="1508839514", oauth_token="HXR...", oauth_version="1.0a"        | 
-----------------------------------------------------------------------------------
```
In the text above, three dots are used to imply more random characters.
The data in the Authorization field should be used as the HTTP header value, exactly as it appears, without further encoding.  

Note: You should always see that, when using HMAC-SHA1, the most common hash algorithm, the signature always ends "...%3D".  



## Author

Steve Hibbert (steve at Continuum dot je), October 2017 (www.continuum.je)



## License

This code is the IP of Continuum Jersey.



## Credits and Acknowledgements

 - Microsoft: For the Hash algos 
 - Twitter: For the explanation of OAuth1