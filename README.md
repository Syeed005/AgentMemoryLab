# AI Lab 02 --- Conversational State & Agent Memory

A focused C#/.NET lab for learning how LLM-based applications maintain
conversation state, long-term memory, and relevant context across
interactions.

## What This Lab Covers

-   Stateless LLM behavior and application-managed conversation state
-   Conversation identity and multi-conversation isolation
-   Persistent users, conversations, and messages
-   Bounded recent conversation context
-   Automatic long-term memory extraction
-   User-scoped and conversation-scoped memory
-   Memory updates, forgetting, and reactivation
-   Memory history and provenance
-   Category-based memory candidate reduction
-   Selective LLM-based memory retrieval
-   Structured outputs for memory extraction and selection
-   Trust-aware memory injection and instruction boundaries
-   Conversation summarization and compaction
-   Concurrent memory selection and extraction
-   Graceful degradation for auxiliary AI operations
-   Token, latency, and memory-retrieval observability

## Example Domain

The lab uses a conversational AI assistant that learns and recalls
structured user preferences and contextual information, such as:

-   `FavoriteProgrammingLanguage`
-   `PreferredCloud`
-   `PreferredIDE`
-   `PreferredRelationalDatabase`
-   `ExplanationStyle`

This keeps the domain simple while focusing on conversational state,
memory lifecycle, retrieval, and context-management behavior.

## Core Flow

``` text
User Request
     ↓
Conversation State
     ├── Recent Messages
     └── Conversation Summary
     ↓
Long-Term Memory
     ├── Candidate Reduction
     ├── Relevant Memory Selection
     └── Scoped Memory Resolution
     ↓
Working Context
     ↓
LLM
     ↓
Response
     ↓
Memory Extraction
     ↓
Memory Persistence / History
```

## Tech Stack

-   C# / .NET
-   ASP.NET Core Web API
-   Microsoft Foundry / Azure-hosted OpenAI
-   OpenAI .NET SDK
-   Entity Framework Core
-   SQL Server

## Status

**Lab 02: Complete.**

Large-scale semantic/vector memory retrieval and advanced memory
evaluation are intentionally deferred to future dedicated labs.
