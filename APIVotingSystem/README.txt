##Configuracoes

1° Verifique no arquivo appsettings se a ConnectionString é igual a da base desejada.
2° Verifique se a Pasta Migrations esta com os dados do banco.
 Se não estiver utilize o comando no Package Manager Console
  [Add-Migration InitialDBCreation]

3° Execute o comando [Updated-DataBase] para criar o Banco na sua base.