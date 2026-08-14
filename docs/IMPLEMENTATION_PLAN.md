# HomeHub Implementation Plan

This plan is intentionally incremental. Each phase should end with a working checkpoint.

## API Integration Rule

Real external API connections happen only after each service has:

1. A provider interface and contract.
2. A fake or in-memory implementation for local tests.
3. Contract tests that prove the adapter contract.
4. A documented fallback strategy for timeout, malformed payloads, and outages.

This keeps Home.Api independent from live providers until the service boundary is stable.

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

1. [x] Create Home.Api and Home.Api.Tests.
2. [x] Add auth baseline (JWT validation layer; Cognito integration can be stubbed for local dev).
3. [x] Add persistence and first entity model.
4. [x] Add one endpoint: GET /api/dashboard/summary.

Exit criteria:

- [x] Endpoint returns seeded local data.
- [x] Unit + integration tests pass.

## Phase 2: Weather Service

Outcome:

- Weather data is fetched through Weather.Service and aggregated by Home.Api.

Tasks:

1. Create Weather.Service + tests.
2. Define IWeatherProvider contract and fake provider for local development.
3. Add real yr.no adapter using HttpClient with timeout, retry, and response mapping.
4. Add Home.Api client call and timeout/retry policy.
5. Add error fallback behavior and provider-selection config.

Exit criteria:

- Dashboard summary includes weather block.
- Failure mode is handled and tested.
- Real API call works in a smoke test against the configured endpoint.

## Phase 3: Transit Service

Outcome:

- Transit departures/arrivals integrated behind Transit.Service.

Tasks:

1. Create Transit.Service + tests.
2. Define ITransitProvider contract and fake provider representing departures and disruptions.
3. Add real Entur provider client and normalization mapping for stop data, departures, and alerts.
4. Add aggregation from Home.Api and handle rate limits / partial data failures.
5. Add provider contract tests for data-shape drift.

Exit criteria:

- Dashboard summary includes transit block.
- Provider data shape changes are isolated in service layer.
- Real API smoke test confirms successful parsing of live departures.

## Phase 4: Energy Service (Open Source API Adapter)

Outcome:

- Energy data works without Nordpool dependency.

Tasks:

1. Define IEnergyPriceProvider interface.
2. Implement a fake/local provider and contract tests.
3. Implement the first real provider adapter from docs/ENERGY_API_OPTIONS.md, such as hvakosterstrommen.no.
4. Add cache strategy, time-window filtering, and fallback behavior.
5. Add provider configuration to switch adapters without changing Home.Api consumers.

Exit criteria:

- Dashboard summary includes energy block.
- Provider can be swapped with config only.
- Real API smoke test verifies live pricing payloads map correctly.

## Phase 4.5: Real API Integration Pass

Outcome:

- Each external provider is live, but isolated behind a stable service contract.

Tasks:

1. Connect Weather.Service to yr.no for live weather data.
2. Connect Transit.Service to Entur for live departures and disruptions.
3. Connect Energy.Service to an open electricity provider for current pricing and daily forecast.
4. Add API-specific configuration, telemetry, retry policies, and operational logging.
5. Validate one end-to-end dashboard load against real endpoints in a non-production environment.

Exit criteria:

- All three provider integrations are live and tested.
- Failures degrade gracefully without breaking the page.
- The dashboard can load with real responses and clear fallback states.

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