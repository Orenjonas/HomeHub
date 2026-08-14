# Architecture And Delivery Decisions

Use this file as a lightweight decision log.

## 2026-08-12

1. Repository model: monorepo.
- Reason: best learning and portfolio visibility for this phase.

2. Runtime baseline: .NET 10 / C# 14.
- Reason: LTS target with current platform features.

3. AWS access model: IAM Identity Center + IAM roles.
- Reason: secure default, no root for daily workflows.

4. Energy provider strategy: adapter abstraction, no Nordpool dependency.
- Reason: reduce lock-in and allow experimentation with open providers.

## 2026-08-14

5. Home profile persistence belongs exclusively to Home.Api.
- Reason: keep database ownership explicit while the supporting services remain provider adapters.

6. EF Core uses PostgreSQL at runtime and SQLite in-memory for focused service tests.
- Reason: exercise relational persistence behavior quickly in unit tests while matching the local Docker runtime database.

7. The initial EF migration is committed under `Home.Api/Persistence/Migrations`.
- Reason: PostgreSQL schema deployment uses an explicit migration rather than an implicit startup database creation step.
