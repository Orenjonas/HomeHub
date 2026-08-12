# HomeHub Copilot Instructions

## Mission

Help implement HomeHub as a learning-oriented monorepo using .NET 10, AWS, and Terraform.

## Core Principles

- Prioritize secure defaults and explain security tradeoffs.
- Keep service boundaries explicit.
- Favor small, incremental pull requests.
- Add tests with every behavior change.
- Explain reasoning in plain language to support learning.

## Architecture Rules

- Home.Api may call service APIs, but should not access service-owned data stores directly.
- Each service owns its own domain models and persistence concerns.
- Shared libraries are allowed only for thin cross-cutting infrastructure (for example messaging envelope, shared logging setup).
- Avoid a large shared domain model package.

## Coding Rules

- Target net10.0.
- Prefer clear code over clever code.
- Keep public APIs documented and version-aware.
- Use cancellation tokens in async I/O methods.
- Validate inputs and return meaningful problem responses.

## Testing Rules

- Unit tests for domain/service logic.
- Integration tests for API endpoints and persistence.
- Add regression tests when fixing bugs.

## AWS and Secrets Rules

- Never hardcode credentials, tokens, or connection strings.
- Use IAM roles for workloads.
- Use environment variables or secret providers only.
- Root account usage should be treated as emergency-only.

## Agent Output Rules

When making a plan, include:

1. Goal
2. Smallest working increment
3. Risks
4. Validation steps

When implementing, include:

1. Files changed
2. Why each change is needed
3. How to test locally
4. What to do next
