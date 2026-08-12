# Architecture Baseline

## Goals

- Fast local development
- Clear service ownership boundaries
- Low-friction path to AWS ECS deployment
- Incremental movement from sync calls to event-driven patterns

## Service Overview

- Home.Api: frontend-facing aggregation API.
- Weather.Service: weather provider integration and normalization.
- Transit.Service: transit provider integration and normalization.
- Energy.Service: electricity price provider adapter and normalization.
- Notification.Worker: asynchronous notification processing.

## Boundaries

- Home.Api can call service APIs.
- Services own transformation logic for provider payloads.
- Do not share domain entities across services.

## Communication

- Early phases: HTTP between Home.Api and supporting services.
- Later phases: events for asynchronous workflows.

## Deployment Model

- Each service packaged as a container.
- ECS services/tasks run with IAM roles.
- Secrets loaded via managed secret store.

## Data Model Direction

- Start with one PostgreSQL database only for Home.Api in early learning phase.
- Avoid cross-service database access.
- Split persistence per service as complexity grows.

## Observability Baseline

- Structured logs from day one.
- Correlation IDs across service calls.
- Health endpoints for all services.
