# Aliftech
First : add users and wallets with Post : https://localhost:44358/api/v1/Users and Post : https://localhost:44358/api/v1/Wallet
You should use https://codebeautify.org/hmac-generator for calculate hash request body for X-Didest and https://www.base64encode.org/ for calculating X-UserId
For calculate hash copy request body what you need in json correct format with key "secret" in may case in project
For X-UserId user's Id:password encode in above website
!!! you should use basic and one space in headers like code below
In case this request correct 

curl --location --request POST 'https://localhost:44358/api/v1/wallet/replenishWallet' \
--header 'X-UserId: basic MTphYWE=' \
--header 'X-Digest: basic df7de3a5ad9a5b565ac20906a3464365955d2c4e' \
--header 'Content-Type: application/json' \
--data-raw '{
  "userId": 1,
  "walletId": 1,
  "amount": 2
}'
when user have 1:aaa id is one , password is aaa
