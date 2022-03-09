# Introduction 
Container que orquestra toda parte de comunicação com a Luz
# Getting Started

## Installation process
### Cloud Formation
- Não esquecer de colocar a flag --profile \<profile desejada\>
#### Desenvolvimento
    sam package --template-file template.yaml --s3-bucket codes-dev --output-template-file deploy.yaml
	
	aws cloudformation deploy --template-file deploy.yaml --stack-name infohub-bpo-dev --parameter-overrides Subnet1=subnet-0d6e7fbe050fbaf42 Subnet2=subnet-0901502758b7be564 SecurityGroupId=sg-0800ff2cd395b462a
#### Uat

    sam package  --template-file template-uat.yaml --s3-bucket edm-code-repository --output-template-file deploy.yaml
	
	aws cloudformation deploy --template-file deploy.yaml --stack-name infohub-bpo-uat --parameter-overrides Subnet1=subnet-07e0381bcc6b813c0 Subnet2=subnet-0db5f2ad856234e19 SecurityGroupId=sg-02fdc411a9e962dd5
#### Produção

    sam package  --template-file template-prod.yaml --s3-bucket edm-code-repository --output-template-file deploy.yaml
	
	aws cloudformation deploy --template-file deploy.yaml --stack-name infohub-bpo-prod --parameter-overrides Subnet1=subnet-0d6e7fbe050fbaf42 Subnet2=subnet-0901502758b7be564 SecurityGroupId=sg-0800ff2cd395b462a

## Software dependencies
## Latest releases
## API references

## Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)