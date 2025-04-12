# WpfTypeEditor

A flexible and extensible **property inspector framework** for WPF — designed to automatically generate editors for object types using reflection and clean MVVM patterns.

## ✨ What is WpfTypeEditor?

**WpfTypeEditor** allows developers to render and edit any C# object in WPF by reflecting on its members and automatically generating matching UI editors — similar to Unity's Inspector. 

It combines attribute-based discovery, type-driven view model factories, and convention-based view generation to drastically reduce UI boilerplate in complex editors and tools.

---

## 🎯 Why I'm Building This

WPF is a powerful UI framework, but building dynamic editor UIs can be repetitive and tedious. Unity solved this problem with its Inspector — and this project brings that same philosophy to general-purpose WPF apps.

The goals are:
- 🔁 **Minimal boilerplate** for property editors
- 📦 **Support for common types** (`string`, `int`, `bool`, `enum`, `List<T>`, etc.)
- 🧠 **Convention-based generation** of ViewModels and Views
- 🧩 **Attribute-driven customization**
- 🔧 **Reflection-powered binding** to any object

Whether you're building internal tools, debugging visualizers, or data-driven apps, this framework gives you **automatic inspectors** for your types.

---

## 🛠 Features

- ✅ Attribute-based `[Export]` system for marking editable members
- 🧠 Convention-based view discovery (e.g., `StringViewModel` → `StringView`)
- 🧰 Built-in editors for:
  - `string`, `int`, `bool`
  - `enum` (with custom display names)
  - `List<T>` (with dynamic item editors)
  - `Delegate` (method buttons)
- 📦 Extensible `ValueViewModelFactory` to register custom editors
- 🧼 Clean MVVM separation
- ⚡ Automatically tracks updates and propagates them with `OnValueChanged`

---

## 🚀 Getting Started

1. Add the `[Export]` attribute to any fields or properties you want exposed in the editor.
2. Create a view model using `ObjectViewModel<T>`.
3. Bind it to a view that uses the `ConventionViewTemplateSelector`.
4. Done! 🎉

---

## 📦 Example

```csharp
public class Person
{
   [Export]
   public string FirstName { get; set; }

   [Export]
   public string LastName { get; set; }

   [Export]
   public City City { get; set; }

   [Export]
   public List<string> Tags { get; set; } = new();
}

var vm = new ObjectViewModel<Person>(new Person { ... });
```
In the UI:
```xml
<ItemsControl ItemsSource="{Binding Fields}"
              ItemTemplateSelector="{StaticResource ViewTemplateSelector}" />
```

## 📁 Project Structure

- **`BaseValueViewModel<T>`**  
  Abstract base class for all typed value editors. Implements change tracking, `INotifyPropertyChanged`, and typed/untyped value access.

- **`ValueViewModelFactory`**  
  Central registry and creation logic for mapping `Type → ViewModel`. Supports generic types, exact matches, and reflection-based auto-registration.

- **`RegisterViewModel` attribute**  
  Declaratively maps types to view models (e.g., `[RegisterViewModel(typeof(string))]`), optionally supporting generic types like `List<>`.

- **`ListViewModel<T>`**  
  A dynamic list editor that supports nested item editors and live synchronization between list and view models.

- **`EnumViewModel<TEnum>`**  
  A drop-down selector for enums, including support for display-friendly descriptions via attributes.

- **`MethodViewModel`**  
  Allows `[Export]` methods (with no parameters) to be rendered as buttons, which invoke the method on click.

- **`ObjectViewModel<T>`**  
  Reflects over `[Export]` fields/properties/methods and builds a list of `LabeledValueViewModel`s that can be data-bound in the UI.

- **`ConventionViewTemplateSelector`**  
  Resolves `*ViewModel` to `*View` automatically by convention (e.g., `StringViewModel → StringView`), including support for generic view models.

- **`ExportAttribute`**  
  Marks a field, property, or method to be exposed in the editor UI.
