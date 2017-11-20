pipeline {
    agent none
    environment {
        REPO     = 'https://github.com/eglookz/jenkins-ci.git'
        SLN_NAME = 'AsposeTestApp.sln'

        NUGET    = "\"C:\\Jenkins\\workspace\\tools\\nuget\\nuget.exe\""
        NUNIT    = "\"C:\\Program Files (x86)\\NUnit.org\\nunit-console\\nunit3-console.exe\""
    }

    stages {
        stage('Preparation') {
            parallel {
                stage('Prepare on common-node') {
                    agent {
                        label 'common-node'
                    }
                    steps {
                        git url: REPO
                        bat("${NUGET} restore \"${SLN_NAME}\"")
                    }
                }
                stage('Prepare on specific-node') {
                    agent {
                        label 'specific-node'
                    }
                    steps {
                        git url: REPO
                        bat("${NUGET} restore \"${SLN_NAME}\"")
                    }
                }
            }
        }
        stage('Build') {
            parallel {
                stage('Build on common-node') {
                    agent {
                        label 'common-node'
                    }
                    steps {
                        bat "${tool 'MSBuild'} ${SLN_NAME} /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
                    }
                }
                stage('Build on specific-node') {
                    agent {
                        label 'specific-node'
                    }
                    steps {
                        bat "${tool 'MSBuild'} ${SLN_NAME} /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
                    }
                }
            }           
        }
        stage('Test') {
            parallel {
                stage('Test on common-node') {
                    agent {
                        label 'common-node'
                    }
                    steps {
                        bat("${NUNIT} \"AsposeTestApp\\bin\\Release\\AsposeTestApp.exe\" --test=AsposeTestApp.CalculatorTest /framework:net-4.5")
                        nunit testResultsPattern: 'TestResult.xml'
                    }
                }
                stage('Test on specific-node') {
                    agent {
                        label 'specific-node'
                    }
                    steps {
                        bat("${NUNIT} \"AsposeTestApp\\bin\\Release\\AsposeTestApp.exe\" --test=AsposeTestApp.SpecificCalculatorTest /framework:net-4.5")
                        nunit testResultsPattern: 'TestResult.xml'
                    }
                }
            }
        }
    }
    post {
        success {
            mail to: 'denis.schelkunov@gmail.com',
            subject: "Pipeline Success: ${currentBuild.fullDisplayName}",
            body: "Build is back to normal (success): ${env.BUILD_URL}"
        }

        failure {
            mail to: 'denis.schelkunov@gmail.com',
            subject: "Failed Pipeline: ${currentBuild.fullDisplayName}",
            body: "Build failed: ${env.BUILD_URL}"
        }
    }
}