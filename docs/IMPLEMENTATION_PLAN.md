# HomeHub Implementation Plan

This plan is intentionally incremental. Each phase should end with a working checkpoint.

## Phase 0: Foundations (Current)

Outcome:

- Repository structure and team/agent workflow docs exist.
- Security and IAM approach is documented.
- Development path is explicit.

Tasks:

1. Keep this plan updated as work progresses.
2. Initialize .NET 10 solution and first API project.
3. Add local development dependencies (PostgreSQL, optional Redis) in docker compose.

Exit criteria:

- Solution builds.
- Home.Api starts locally.

## Phase 1: Home.Api First Vertical Slice

Outcome:

- Authenticated API with basic user/home dashboard endpoint.

Tasks:

1. Create Home.Api and Home.Api.Tests.
2. Add auth baseline (JWT validation layer; Cognito integration can be stubbed for local dev).
3. Add persistence and first entity model.
4. Add one endpoint: GET /api/dashboard/summary.

Exit criteria:

- Endpoint returns seeded local data.
- Unit + integration tests pass.

## Phase 2: Weather Service

Outcome:

- Weather data is fetched through Weather.Service and aggregated by Home.Api.

Tasks:

1. Create Weather.Service + tests.
2. Integrate one weather provider client.
3. Add Home.Api client call and timeout/retry policy.
4. Add error fallback behavior.

Exit criteria:

- Dashboard summary includes weather block.
- Failure mode is handled and tested.

## Phase 3: Transit Service

Outcome:

- Transit departures/arrivals integrated behind Transit.Service.

Tasks:

1. Create Transit.Service + tests.
2. Add provider client and normalization mapping.
3. Add aggregation from Home.Api.

Exit criteria:

- Dashboard summary includes transit block.
- Provider data shape changes are isolated in service layer.

## Phase 4: Energy Service (Open Source API Adapter)

Outcome:

- Energy data works without Nordpool dependency.

Tasks:

1. Define IEnergyPriceProvider interface.
2. Implement first provider adapter from docs/ENERGY_API_OPTIONS.md.
3. Add cache strategy and fallback behavior.
4. Add provider contract tests.

Exit criteria:

- Dashboard summary includes energy block.
- Provider can be swapped with config only.

## Phase 5: Messaging + Worker

Outcome:

- Event-driven notifications supported.

Tasks:

1. Create Notification.Worker.
2. Define event envelope and event contracts.
3. Add queue consumption and notification dispatch.

Exit criteria:

- End-to-end local event path is tested.

## Phase 6: Infrastructure + CI/CD

Outcome:

- Automated build/test/deploy baseline.

Tasks:

1. Add Terraform modules for networking, compute, data, messaging, iam.
2. Add GitHub Actions for PR tests and main branch deployment flow.
3. Add environment strategy (dev/stage/prod).

Exit criteria:

- One environment deploys from pipeline.

## Weekly Cadence Suggestion

- Week 1: Phase 1
- Week 2: Phase 2
- Week 3: Phase 3
- Week 4: Phase 4
- Week 5: Phase 5
- Week 6: Phase 6

Adjust pace based on learning depth, not speed.
