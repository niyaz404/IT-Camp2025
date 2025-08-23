#!/bin/bash
set -e

sleep 15  

TOKEN=$(curl -s -X POST \
  "http://keycloak:8080/realms/master/protocol/openid-connect/token" \
  -d "username=admin" \
  -d "password=admin" \
  -d "grant_type=password" \
  -d "client_id=admin-cli" \
  | jq -r .access_token)

curl -X PUT "http://keycloak:8080/admin/realms/preditrix/user-profile" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $TOKEN" \
  -d '{
    "attributes": [
      {
        "name": "middleName",
        "displayName": "Отчество",
        "required": {
          "roles": ["user"]
        },
        "permissions": {
          "view": ["admin","user"],
          "edit": ["admin","user"]
        }
      }
    ]
  }'