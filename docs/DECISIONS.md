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
