import os
import re

def get_version_input(prompt):
    value = input(prompt).strip()
    if not value:
        return None
    if not re.match(r'^\d+\.\d+\.\d+$', value):
        print("Invalid format! Use X.Y.Z (e.g. 3.0.1) or leave blank.")
        return get_version_input(prompt)
    parts = value.split('.')
    return f'new Version({", ".join(parts)})'

def update_plugin_cs(filepath, version, exiled_version):
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()

    changed = False

    if version:
        new_content, count = re.subn(
            r'(public\s+override\s+Version\s+Version\s*\{\s*get;\s*\}\s*=\s*)new Version\([^)]+\);',
            rf'\1{version};',
            content
        )
        if count > 0:
            content = new_content
            changed = True
        else:
            print(f"Plugin version not found in: {filepath}")

    if exiled_version:
        new_content, count = re.subn(
            r'(public\s+override\s+Version\s+RequiredExiledVersion\s*\{\s*get;\s*\}\s*=\s*)new Version\([^)]+\);',
            rf'\1{exiled_version};',
            content
        )
        if count > 0:
            content = new_content
            changed = True
        else:
            print(f"RequiredExiledVersion not found in: {filepath}")

    if changed:
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f"Updated: {filepath}")

def main():
    print("Enter new plugin version (X.Y.Z, e.g. 3.0.1, leave blank to skip):")
    plugin_version = get_version_input("Plugin version: ")
    print("Enter required Exiled version (X.Y.Z, e.g. 9.8.0, leave blank to skip):")
    exiled_version = get_version_input("RequiredExiledVersion: ")

    parent_dir = os.getcwd()
    for folder in os.listdir(parent_dir):
        folder_path = os.path.join(parent_dir, folder)
        if os.path.isdir(folder_path):
            plugin_file = os.path.join(folder_path, "Plugin.cs")
            if os.path.isfile(plugin_file):
                update_plugin_cs(plugin_file, plugin_version, exiled_version)
            else:
                print(f"No Plugin.cs file found in: {folder_path}")

if __name__ == "__main__":
    main()