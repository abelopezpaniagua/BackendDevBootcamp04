
# Configuration


Change in appsettings.json at **EmailMicroservice**
```bash
"ConnectionStrings": {
    "rabitmq": "rabbitmq://localhost"
},
"MailSettings": {
    "Mail": "YOUR_MAIL",
    "DisplayName": "YOUR_NAME",
    "Password": "YOUR_PASSWORD",
    "Host": "smtp.gmail.com",
    "Port": 587
  }
```
Change in appsettings.json at **CampaignMicroservice**
```bash
"ConnectionStrings": {
    "myconn": "server=127.0.0.1;port=3306;database=campaign_db;user=root;password=YOUR_PASSWORD"
    "rabitmq": "rabbitmq://localhost",
}
```
