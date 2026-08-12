# HomeHub Agent Playbook

This file defines how human + Copilot agents should collaborate in this repository.

## Working Mode

- Build one vertical slice at a time.
- Keep each slice deployable or very close to deployable.
- Prefer learning clarity over premature optimization.

## Standard Task Loop

1. Define scope for one small outcome.
2. Write or update acceptance criteria.
3. Implement minimal code change.
4. Add or update tests.
5. Document decisions in docs/DECISIONS.md.
6. Capture learning notes in repository memory.

## Memory Workflow

Use memory scopes intentionally:

- User memory: long-term personal preferences and repeated workflow patterns.
- Session memory: current task notes and temporary plan details.
- Repository memory: HomeHub-specific conventions, commands, and architecture decisions.

After each meaningful session, record:

- What was built
- What failed and why
- Final commands that worked
- Next checkpoint

## Prompt Templates

### Implement Feature Slice

Goal: Implement <feature> in the smallest working increment.
Constraints: net10.0, tests required, no secrets in code.
Deliver: code, tests, docs updates, and local run instructions.

### Debug Failure

Issue: <error summary>
Need: root cause, minimal fix, regression test, and explanation of why fix works.

### Architecture Decision

Decision topic: <topic>
Need: options, tradeoffs, recommendation, impact, and follow-up tasks.

## Definition Of Done

A task is done when:

- Functionality works locally.
- Tests pass for changed behavior.
- Docs are updated where needed.
- Security posture is not weakened.
- Next step is clearly identified.
