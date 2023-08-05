# TechWiz
#File appsettings.json sẽ bị bỏ qua khi push từ local lên github!!
Sau khi clone dự án về máy thì: 
B1: Vào Package Manager Console =>  chạy lệnh: update-database -Context UserManagerContext 
B2: Vào View => Termial => chạy 2 lệnh : - dotnet user-secrets set "Authentication:Google:ClientId" "798615036886-fqsap81alleni2b968ghrp0smoo14ruc.apps.googleusercontent.com"
                                        - dotnet user-secrets set "Authentication:Google:ClientSecret" "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"
