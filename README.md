# aspapi

some endpoints

AUTH
1) (post) https://app-walks-eastus-dev-001.azurewebsites.net/api/Auth/Register
  {
    "userName": "user@example.com",
    "password": "string",
    "roles": ["Writer","Reader"]
  }
2) (post) https://app-walks-eastus-dev-001.azurewebsites.net/api/Auth/Login
  {
    "userName": "user@example.com",
    "password": "string"
  }
  
IMAGE UPLOAD
3) (post) https://app-walks-eastus-dev-001.azurewebsites.net/api/Images/Upload
  File * string($binary)
  FileName *string

REGIONS  
4) (post) https://app-walks-eastus-dev-001.azurewebsites.net/api/Regions
{
  "code": "str",
  "name": "string",
  "regionImageUrl": "string"
}
5) (get) https://app-walks-eastus-dev-001.azurewebsites.net/api/Regions
6) (get) https://app-walks-eastus-dev-001.azurewebsites.net/api/Regions/region.id
7) (put) https://app-walks-eastus-dev-001.azurewebsites.net/api/Regions/region.id
8) (delete) https://app-walks-eastus-dev-001.azurewebsites.net/api/Regions/region.id
