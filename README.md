# HomeHub

HomeHub is a learning-focused, production-style smart home platform built as a monorepo.

It is designed to help you learn and demonstrate:

- .NET 10 and C# 14
- Service boundaries and microservice communication
- Authentication and authorization
- Docker and AWS deployment patterns
- Infrastructure as code with Terraform
- CI/CD with GitHub Actions
- Working effectively with GitHub Copilot agents

## Repository Goals

- Keep one repository, multiple independently deployable services.
- Prefer secure defaults (IAM roles, no long-lived keys, no secrets in git).
- Build incrementally: one working slice at a time.
- Keep architecture decisions documented.

## Planned Structure

```text
HomeHub/
  src/
    Home.Api/
    Weather.Service/
    Transit.Service/
    Energy.Service/
    Notification.Worker/
  tests/
    Home.Api.Tests/
    Weather.Service.Tests/
    Transit.Service.Tests/
    Energy.Service.Tests/
  frontend/
    home-dashboard/
  infrastructure/
    terraform/
  docker/
  docs/
  .github/
    workflows/
```

## Build Order (Recommended)

1. Home.Api + PostgreSQL + auth foundation.
2. Weather.Service integration and Home.Api aggregation.
3. Transit.Service integration.
4. Energy.Service with provider adapter abstraction.
5. Notification.Worker with SNS/SQS style event workflow.
6. Terraform + GitHub Actions deployment pipeline.

See docs for full details:

- docs/IMPLEMENTATION_PLAN.md
- docs/COPILOT_WORKFLOW.md
- docs/ARCHITECTURE.md
- docs/ENERGY_API_OPTIONS.md

## Security Basics

- Use IAM Identity Center for day-to-day AWS access.
- Reserve root account for emergency/account-level operations only.
- Use IAM roles for workloads (ECS tasks/workers), not static credentials.
- Keep secrets in AWS Secrets Manager or SSM Parameter Store.

## Quick Start (Current Phase)

1. Confirm .NET 10 SDK is installed.
2. Create initial solution and Home.Api service.
3. Set up local PostgreSQL via docker compose.
4. Implement first authenticated endpoint.
5. Add tests and GitHub workflow.

## Copilot-First Workflow

This repo includes Copilot guidance files so agents can help you build and learn consistently.

Start in docs/COPILOT_WORKFLOW.md and docs/IMPLEMENTATION_PLAN.md.
