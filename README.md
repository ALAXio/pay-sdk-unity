This repo contains a work in progress, we're working hard to release v. 1.0 in the next days to come. 

Expected stable realese date: September 25th, 2019.

# ALAX Pay SDK for Unity #

Coming soon to Unity Asset Store as a free asset!

## Introduction ##

ALAX Pay is a secure online platform for transferring and storing ALAX digital currency. Our mission is to create an open financial system for the gaming world by providing services for sending or receiving ALAX digital currency between online wallets, game players, or game publishers. ALAX Pay is a platform on which many applications are being built using our SDK.

This SDK represents a Unity3D version of the official ALAX Pay SDK (https://github.com/ALAXio/alax-pay-sdk). It is based on the official ALAX Pay SDK, and this repository is always syncronized with each new release of the official SDK.

### Installation guide ###
#### Minimum requirements ####
Minimum requirements for the SDK are:
Unity3D 
Android SDK Version >= 4.1.
API >= 16.
Android Studio and Gradle installed
ALAX Pay application installed on the test device

Only Unity3D versions 2017.1+ are supported as development environment.



### Components ###

#### ALAX Pay Wallet App ####
The ALAX Pay Wallet is a precompiled application that provides the services that the ALAX Pay SDK utilises. Without it being installed and running, applications built on the ALAX Pay SDK will not function.

In order to test applications written with this SDK, developers will need to create at least two accounts through the wallet so that they can transfer assets between them. You’ll need to contact Alax Support Team via email address `support@alax.io` to request some tokens for testing.

##### Determining a Wallet Address / Account Name #####

Start the ALAX Pay Wallet application.
Sign in using the  telephone number and password that the desired account was created with..
Tap the `Top Up` button. A QR code for ALXT Top Up will be displayed with the account’s ‘Wallet Address’ displayed in a box below it. This can then be copied to the clipboard using the supplied icon.

The process for a newly created account is slightly different:

Start the ALAX Pay Wallet application.
* If it is already ‘signed in’, you will need to ‘sign out’ by selecting the ‘sign out’ option from the menu accessible via the three vertical dot icon in the top right hand corner of the screen.
* With the Wallet in a ‘signed out’ state, select the `Don’t have an account? Sign Up!` link at the bottom of the display.
* Enter your telephone number, followed by the password, twice. Follow all the other instructions to create an account. After successful sign up you will then be signed in.
* Hit the `Top Up` button to display the QR Code and Wallet Address.

Selecting `Top Up`, as above, will display the Wallet Address / Account Name which, as before, can then be copied to the clipboard using the supplied icon.

#### ALAX Pay for Unity SDK ####
The SDK is published as a public repository on github, as well as a free asset is coming soon to Unity Asset Store. In order to integ

#### ALAX Pay Example Application ####


### Using the ALAX Pay SDK ###

#### Initialization ####
Before any of the SDK methods can be used, the SDK service needs to be registered with the operating system using the statement:
```
	AlaxPay.init()
```

This need only be done once during the duration of the application, and must be done before calling any other SDK methods.

See `fun onCreate()` in the provided example application for context.

#### Available Methods ####
ALAX Pay SDK for Unity provides a script called AlaxIAPManager.cs. Create an empty object on a scene and add this script to it. The script provides it's own inapps related fields into the Editor's inspector. These fields are used to set up the IAP Manager.   
