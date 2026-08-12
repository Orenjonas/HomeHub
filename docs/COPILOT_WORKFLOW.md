# Copilot Workflow For Learning And Delivery

## Purpose

Use Copilot agents to both implement features and explain tradeoffs so learning accumulates over time.

## Session Start Checklist

1. Confirm current phase in docs/IMPLEMENTATION_PLAN.md.
2. Define one small outcome for this session.
3. State acceptance criteria before coding.

## High-Value Prompts

### Build Prompt

Implement Phase <N> task <task-name> as the smallest working increment.
Include tests, update docs, and explain design choices.

### Review Prompt

Review recent changes for bugs, regressions, security risks, and missing tests.
Prioritize findings by severity and cite file locations.

### Learn Prompt

Teach me this implementation:
- architecture reasoning
- key .NET patterns used
- common pitfalls
- how this maps to AWS deployment later

## Memory Practice

At the end of a session, capture:

- Final architecture decisions
- Commands that worked
- Test strategy used
- Open questions for next session

Store repository-specific facts in repository memory so future sessions can reuse context quickly.

## Pull Request Pattern

1. Small scope branch from main.
2. Keep PR focused on one outcome.
3. Include:
   - what changed
   - why it changed
   - how it was tested
   - next step in plan

## Learning Rule

If implementation is complex, ask Copilot for:

- a simplified explanation
- one alternative design
- reason the chosen approach is better now
