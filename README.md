# AI Lab 02 — Conversational State & Agent Memory

A hands-on C#/.NET lab exploring how conversational AI applications maintain state and long-term memory around a stateless LLM.

## Objective

Build and understand the core mechanisms required for a stateful enterprise AI assistant, including conversation persistence, bounded context, long-term memory, selective retrieval, memory lifecycle, conversation summarization, observability, and graceful failure handling.

## Tech Stack

- C# / ASP.NET Core
- Microsoft Foundry / Azure-hosted OpenAI
- OpenAI .NET SDK
- SQL Server
- Entity Framework Core

## Key Features

- Persistent users, conversations, and messages
- Token/size-aware recent conversation context
- Conversation summarization and compaction
- Automatic long-term memory extraction
- User-scoped and conversation-scoped memory
- Memory update, forgetting, and reactivation
- Memory history and provenance
- Category-based candidate reduction
- LLM-based selective memory retrieval
- Structured JSON outputs
- Trust-aware memory injection
- Concurrent memory selection and extraction
- Token and latency observability
- Graceful degradation for auxiliary AI operations

## State Architecture

The lab separates conversational state into three layers:

1. **Recent Conversation** — high-fidelity short-term context
2. **Conversation Summary** — compressed older conversation context
3. **Long-Term Memory** — durable facts, preferences, and contextual information

```text
User Request
     |
     +--> Recent Conversation
     |
     +--> Conversation Summary
     |
     +--> Long-Term Memory
              |
              +--> Candidate Reduction
              +--> Relevant Memory Selection
     |
     v
Working Context
     |
     v
LLM
     |
     v
Response
