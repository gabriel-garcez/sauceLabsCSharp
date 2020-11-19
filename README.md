# sauceLabsCSharp
A simple C# framework using Sauce Labs to do a test.


*** Settings ***
Library  SeleniumLibrary

*** Variables ***


*** Test Cases ***
LoginTest
    open browser    http://demo.nopcommerce.com     chrome
    click link  xpath://a[@class='ico-login']
    input text   id:Email   test@test.com
    input text  id:Password     Test@123
    click element   xpath://input[@class='button-1 login-button']
    close browser

*** Keywords ***
