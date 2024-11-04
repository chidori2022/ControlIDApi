# ControlIDApi

Projeto para integração com um leitor biométrico usando a `libcidbio.dll`. Este guia fornece as instruções necessárias para clonar, configurar e rodar o projeto como um serviço no Windows.

## Pré-requisitos

Antes de começar, certifique-se de ter:
- [Git](https://git-scm.com/downloads) instalado para clonar o repositório.
- [.NET SDK](https://dotnet.microsoft.com/download) instalado (este projeto usa .NET 8.0).
- A biblioteca `libcidbio.dll` disponível (veja as instruções abaixo).

Links da ControlID:
- [DOCUMENTAÇÃO](https://www.controlid.com.br/docs/idbio-pt/4_csharp_reference/)
- [DRIVER](https://www.controlid.com.br/idbio/windows_driver.zip)
- [SDK](https://www.controlid.com.br/idbio/IDBIO_SDK.zip)


## Passo a Passo de Instalação

## 1. Clonar o Repositório

Clone o repositório para a sua máquina local usando o comando:

```bash
git clone https://github.com/<seu-usuario>/ControlIDApi.git

Substitua <seu-usuario> pelo nome de usuário do GitHub onde o projeto está hospedado.
```
## 2. Navegar até o Diretório do Projeto
Entre no diretório do projeto clonado:
```bash
cd ControlIDApi
```
## 3. Restaurar as Dependências
```bash
dotnet restore
```
## 4. Configurar a libcidbio.dll

Este projeto depende da biblioteca libcidbio.dll. Para garantir que a DLL seja encontrada pelo sistema:

# Coloque a libcidbio.dll no diretório System32:
```bash
copy <caminho-da-dll>\libcidbio.dll C:\Windows\System32
```
Substitua <caminho-da-dll> pelo local onde a libcidbio.dll está armazenada.

Nota: Isso é necessário para que o serviço encontre a DLL quando for executado em segundo plano.

## 5. Compilar e Publicar o Projeto

Para rodar o projeto como um serviço, será necessário publicá-lo primeiro:

```bash
dotnet publish -c Release -r win-x86 --self-contained true -p:PublishSingleFile=true
```
Esse comando compilará o projeto em modo de Release e criará um executável independente, incluindo todas as dependências.

## 6. Executar a Aplicação Localmente

Para testar a aplicação, execute o arquivo ControlIDApi.exe gerado na pasta publish:

```bash
cd bin\Release\net8.0\win-x86\publish\ControlIDApi.exe
```
Isso iniciará o servidor localmente. Teste os endpoints no Postman ou no navegador acessando http://localhost:5000.

## 7. Registrar como Serviço do Windows (Opcional)

Para rodar o projeto como um serviço do Windows em segundo plano:
### Abra o Prompt de Comando como Administrador.
### Registre o serviço com o comando
```bash
sc.exe create "ControlID" binPath= "C:\ControlIDApi\bin\Release\net8.0\win-x86\publish\ControlIDApi.exe" start= auto
```
Isso iniciará o servidor localmente. Teste os endpoints no Postman ou no navegador acessando http://localhost:5000.

Eu coloquei na pasta C: do computador, atente o caminho do seu projeto.

### Inicie o serviço com o comando:
```bash
sc start "ControlID"
```

Agora, o serviço estará rodando em segundo plano e será iniciado automaticamente com o sistema.

## 8. Verificar o Funcionamento da API
Com o serviço em execução, você pode acessar os endpoints usando um cliente HTTP como Postman. Exemplo de endpoint:
```bash
http://localhost:5000/api/biometria/inicializar
```

### Considerações Finais
- Permissões: Certifique-se de que o diretório do executável (C:\ControlIDApi) possui permissões adequadas para que o serviço acesse os arquivos.
- Atualizações: Para atualizar o serviço com novas versões do código, basta substituir o executável e reiniciar o serviço.
- Logs: Se possível, configure logs para facilitar o monitoramento do serviço em produção.

### DICAS
- A DLL roda em x32;
- Caso dê algum erro ao iniciar o projeto compilado, tente ver nos eventos do windows para verificar o erro;
- A DLL precisa estar na pasta system32;
- Usei o postman para testar a inicialização e o identificar a biometria, ainda não fiz a funcao cadastrar dele.

Minhas consultas:

```bash
(http://localhost:5000/api/biometria/inicializar)
```

```bash
http://localhost:5000/api/biometria/identificar
```
