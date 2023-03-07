# [futureme.org](futureme.org) API Server Clone

This project is a clone of the [futureme.org](futureme.org) API server. It consists of two services:


* **EmailScheduler**: This service is responsible for scheduling and publishing emails to a Kafka topic named `"emails"`.

* **EmailSender**: This service is responsible for consuming published messages from the `"emails"` Kafka topic and sending those emails to the given receiver email. For now, it outputs email details to the console for testing purposes only!

# Getting Started

**Prerequisites**


* Docker
* Docker Compose

**Installing**

1. Clone this repository to your local machine.
2. Navigate to the root of the project (Default name for root project should be : `Future-me-clone`).
3. Run `docker-compose build` to build the Docker images.
4. Run `docker-compose up` to start the services.
5. Open up another terminal instance and run `docker-compose -f .\docker-compose.liquibase.yml up` to apply liquibase changesets to postgres database
6. Open up browser and type this url in there `http://localhost:8000/swagger` to see beatiful swagger documentation, and play with it!

# Usage

**EmailScheduler**

To schedule an email, send a POST request to the following endpoint:

```
POST http://localhost:8000/Email/AddEmail
```

The request body should be in the following format:

```
json: 
{
    "message": "Your message here",
    "receiverEmail": "receiver@example.com",
    "deliveryDate": "2023-03-08T00:00:00Z"
}
```

If the delivery date is today, the email will be published to the `"emails"` Kafka topic immediately.

**EmailSender**

The `EmailSender` service runs in the background and automatically consumes messages from the `"emails"` Kafka topic. If a message is consumed, it will output the email details to the console.

**Quartz.NET**

The `EmailScheduler` service uses `Quartz.NET` to schedule a task that checks for unsent emails every minute. If there are any unsent emails, the job will publish them to the `"emails"` Kafka topic.

## Architecture diagram:

Project's high level architecture diagram can be found [here](https://drive.google.com/file/d/10qdwKdouTHQNnOHjgjvOxF-9PFz01Cqu/view?usp=sharing)

## Future improvements

1. Use third party services for sending real email messages and make them such that if third party service fails then `EmailSender` service should use another one as an alternative!
2. Reduce database calls especially reduce update queries in Quartz job!
3. Implement auto version liquibase changeset- versioning using powershell script!
4. Add `nginx` configuration to docker-compose 