pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                echo 'Build successful'
            }
        }
    }

    post {
        always {
            emailext(
                to: 'jaferalminshad@gmail.com',
                subject: 'Jenkins Build Log',
                body: 'Build completed. Log attached.',
                attachLog: true
            )
        }
    }
}
