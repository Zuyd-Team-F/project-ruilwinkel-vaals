[![Build & Tests](https://github.com/Zuyd-Team-F/project-ruilwinkel-vaals/actions/workflows/build-tests.yml/badge.svg?branch=development)](https://github.com/Zuyd-Team-F/project-ruilwinkel-vaals/actions/workflows/build-tests.yml)
[![CodeQL](https://github.com/Zuyd-Team-F/project-ruilwinkel-vaals/actions/workflows/codeql-analysis.yml/badge.svg?branch=development)](https://github.com/Zuyd-Team-F/project-ruilwinkel-vaals/actions/workflows/codeql-analysis.yml)
[![Sonar Scan](https://github.com/Zuyd-Team-F/project-ruilwinkel-vaals/actions/workflows/sonar.yml/badge.svg)](https://github.com/Zuyd-Team-F/project-ruilwinkel-vaals/actions/workflows/sonar.yml)

# Requirements

To be able to work in this project, you'll need docker. Docker will handle all the dependancies that are tied in to this project. This will also provide you the free choice of using whatever text editor / IDE you like.

[<img src="https://ms-azuretools.gallerycdn.vsassets.io/extensions/ms-azuretools/vscode-docker/1.12.1/1618259060082/Microsoft.VisualStudio.Services.Icons.Default" width="200">](https://www.docker.com/products/docker-desktop)

# Running the Project

Before attempting to run this project, you'll need to provide environment variables. An example of this is provided with the '.env.example' file. All you have to do is copy this file and rename it to '.env'. You can then change the desired secrets in this file for your local development (This isn't necesarry). An easy way to copy & rename this file can be done with the following command:

```console
cp .env.example .env
```

To run this project, you need to open a CLI (Or whatever code editor you want to use, IDE Visual Studio works aswell) and navigate to the project folder. When there, enter the following code:

```console
docker-compose up
```

That's the only thing you'll need to do and it'll run the project.
