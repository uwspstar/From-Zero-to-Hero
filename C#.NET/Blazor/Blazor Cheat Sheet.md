### Blazor Cheat Sheet

Below is a **Blazor Cheat Sheet** that provides a side-by-side comparison of regular HTML tags and their Blazor equivalents, with explanations and code examples in a tabular format.

---

| **HTML Tag**       | **Blazor Equivalent**               | **Explanation**                                                                                           | **Code Example**                                                                                                                                                                        |
|---------------------|-------------------------------------|-----------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `<form>`            | `<EditForm>`                      | In Blazor, `<EditForm>` wraps form elements to provide validation and model binding functionality.         | **HTML**:<br>`<form>`<br>`<input type="text" />`<br>`</form>`<br>**Blazor**:<br>`<EditForm Model="@userModel">`<br>`<InputText @bind-Value="userModel.Name" />`<br>`</EditForm>`         |
| `<input>`           | `<InputText>`, `<InputNumber>`     | Use Blazor components for data binding and type-safe validation.                                           | **HTML**:<br>`<input type="text" />`<br>**Blazor**:<br>`<InputText @bind-Value="userModel.Name" />`                                                                                     |
| `<select>`          | `<InputSelect>`                   | For dropdowns in Blazor, `<InputSelect>` allows two-way data binding.                                      | **HTML**:<br>`<select>`<br>`<option>Option 1</option>`<br>`</select>`<br>**Blazor**:<br>`<InputSelect @bind-Value="userModel.SelectedOption">`<br>`<option>Option 1</option>`<br>`</InputSelect>` |
| `<textarea>`        | `<InputTextArea>`                 | Replace text areas with `<InputTextArea>` for binding long text inputs.                                    | **HTML**:<br>`<textarea></textarea>`<br>**Blazor**:<br>`<InputTextArea @bind-Value="userModel.Description" />`                                                                           |
| `<button>`          | `<button>` (unchanged)            | Buttons can remain the same in Blazor but often include `@onclick` for event handling.                     | **HTML**:<br>`<button>Submit</button>`<br>**Blazor**:<br>`<button @onclick="HandleSubmit">Submit</button>`                                                                              |
| `<label>`           | `<label>` (unchanged)             | Labels can remain the same, often used with Blazor components.                                             | **HTML**:<br>`<label for="name">Name</label>`<br>**Blazor**:<br>`<label for="name">Name</label>`                                                                                         |
| `<div>`             | `<div>` (unchanged)               | Divs remain unchanged and can hold Blazor components.                                                      | **HTML**:<br>`<div>Content</div>`<br>**Blazor**:<br>`<div>@content</div>`                                                                                                               |
| `<ul>` & `<li>`     | `<ul>` & `<li>` (unchanged)       | Lists remain unchanged, but Blazor uses `@foreach` for dynamic list generation.                            | **HTML**:<br>`<ul><li>Item</li></ul>`<br>**Blazor**:<br>`<ul>@foreach (var item in items) {<li>@item</li>}</ul>`                                                                        |
| `<table>`           | `<table>` (unchanged)             | Tables remain unchanged but leverage Blazor for dynamic row generation.                                    | **HTML**:<br>`<table><tr><td>Cell</td></tr></table>`<br>**Blazor**:<br>`<table><tr>@foreach (var cell in cells) {<td>@cell</td>}</tr></table>`                                          |
| `<input type="checkbox">` | `<InputCheckbox>`           | Use `<InputCheckbox>` for checkbox input binding and state management.                                     | **HTML**:<br>`<input type="checkbox" />`<br>**Blazor**:<br>`<InputCheckbox @bind-Value="userModel.IsChecked" />`                                                                         |
| `<input type="radio">` | `<InputRadio>` & `<InputRadioGroup>` | For radio buttons, use `<InputRadioGroup>` for a collection and `<InputRadio>` for individual options.      | **HTML**:<br>`<input type="radio" name="group1" />`<br>**Blazor**:<br>`<InputRadioGroup @bind-Value="selectedOption"><InputRadio Value="Option1" /></InputRadioGroup>`                   |
| `<img>`             | `<img>` (unchanged)               | The `<img>` tag remains unchanged but can use `@` for dynamic binding.                                     | **HTML**:<br>`<img src="image.png" />`<br>**Blazor**:<br>`<img src="@imagePath" />`                                                                                                     |
| `<a>`               | `<NavLink>`                      | Use `<NavLink>` for navigation with built-in active link support in Blazor.                                | **HTML**:<br>`<a href="/home">Home</a>`<br>**Blazor**:<br>`<NavLink href="/home">Home</NavLink>`                                                                                        |

---

### Example: Full HTML-to-Blazor Form Conversion

#### HTML
```html
<form action="/submit" method="post">
  <label for="name">Name:</label>
  <input type="text" id="name" name="name" />
  <button type="submit">Submit</button>
</form>
```

#### Blazor
```razor
<EditForm Model="@userModel" OnValidSubmit="HandleSubmit">
  <label for="name">Name:</label>
  <InputText id="name" @bind-Value="userModel.Name" />
  <button type="submit">Submit</button>
</EditForm>

@code {
    private UserModel userModel = new UserModel();

    private void HandleSubmit()
    {
        Console.WriteLine($"Name: {userModel.Name}");
    }

    private class UserModel
    {
        public string Name { get; set; }
    }
}
```

This table provides a handy reference for quickly adapting your HTML knowledge to Blazor, alongside practical examples for clarity.
