### User Guide: Using Mermaid Diagrams in Microsoft Loop

Mermaid syntax in Microsoft Loop enables you to create diagrams, flowcharts, and other visuals directly in your collaborative documents. Follow this guide to get started with Mermaid in Loop.

---

#### 1. **Check for Mermaid Support**
   - **Note**: Microsoft Loop is gradually rolling out support for Mermaid syntax, so availability may vary across devices. If you don’t see it on one device, try another or check for updates.

#### 2. **Creating a Mermaid Diagram**
   1. **Open Microsoft Loop** and navigate to the page where you want to insert a Mermaid diagram.
   2. **Insert a Code Block**:
      - Type `/code` to bring up the code block option.
      - Select the code block and set the language to `mermaid`.
   3. **Add Mermaid Syntax**:
      - Within the code block, write your Mermaid code to generate the desired diagram. For example:
        ```mermaid
        graph TD;
          A-->B;
          B-->C;
          C-->D;
          D-->A;
        ```
      - This syntax will create a circular flowchart connecting nodes A, B, C, and D.
      
#### 3. **Diagram Examples**
   - **Flowchart**: Basic flow of nodes and links:
     ```mermaid
     flowchart LR
       Start --> Process1 --> Decision{Is it correct?} 
       Decision -- Yes --> End
       Decision -- No --> Process1
     ```
   - **Gantt Chart**:
     ```mermaid
     gantt
       title Project Timeline
       section Development
       Task1 :a1, 2023-01-01, 30d
       Task2 :after a1, 20d
     ```
   - **Sequence Diagram**:
     ```mermaid
     sequenceDiagram
       Alice->>Bob: Hello Bob, how are you?
       Bob-->>Alice: I'm good, thanks!
     ```

#### 4. **Testing and Adjusting Diagrams**
   - **Render**: Microsoft Loop may automatically render the diagram. Check the display to ensure it appears as expected.
   - **Adjust**: Modify the syntax if necessary to correct or enhance the diagram’s appearance.

#### 5. **Adding Reference Links for Learning**
   - To reference a video guide or other resources, paste the link in your Loop document. For example, [How to Make Flowcharts in Microsoft Loop (YouTube Video)](https://www.youtube.com/watch?v=XZ5p_24uDvo) can be pasted directly into Loop for team members to access easily.

---

With these steps, you can leverage Mermaid in Microsoft Loop to create visual representations that enhance documentation, simplify complex information, and boost collaboration across your team.
