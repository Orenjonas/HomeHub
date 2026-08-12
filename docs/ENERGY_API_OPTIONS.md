# Energy API Options (Without Nordpool)

This project should hide provider differences behind one adapter interface.

## Interface Direction

Define a provider contract in Energy.Service:

- GetCurrentPrice(area, timestamp)
- GetDailyPrices(area, date)

Then implement providers as pluggable adapters.

## Candidate Providers To Evaluate

1. hvakosterstrommen.no
- Popular open endpoint used for Nordic spot-price style data.
- Low friction for quick prototyping.
- Validate uptime and terms before production usage.

2. entsoe transparency API
- Official European power market data source.
- Rich dataset but typically requires registration/token.
- Better long-term reliability than scraping-style endpoints.

3. Country-specific open data portals
- Some markets publish open energy/price data.
- Often stable but format differs by country.

## Selection Criteria

- Legal usage terms
- Data freshness and uptime
- Historical data availability
- Region coverage needed for HomeHub
- Simplicity of integration and maintenance

## Recommendation For First Iteration

Start with hvakosterstrommen.no adapter for speed, then add a second adapter later to prove provider portability.

## Testing Strategy

- Contract tests against IEnergyPriceProvider.
- Provider integration tests with recorded payload fixtures.
- Fallback tests for provider outage or malformed payload.
