# Hornbill

Script Language Javascript Provider for Rhino3d

## Requirements

* [Rhino 7](https://www.rhino3d.com/download/rhino-for-windows/7/latest)
* [Installed Hornbill in Rhino 7 PackageManager](#Step1)
* [node.js](https://nodejs.org/en/) >= 12
* [yarn](https://classic.yarnpkg.com/en/docs/install) >= 1.22.0
* code editor like [VS Code](https://code.visualstudio.com/) for scripting

## Usage

### Step1

* Open Rhino 7
* Typing `PackageManager` command
* Searching for `Hornbill` and install
* Restart Rhino 7
* Keep Rhino window open
* You are good to go

### Step2

* Create a empty folder for store your work.

```bash
mkdir hornbill-test && cd hornbill-test
```

* Initialing this folder as a javascript module.

```bash
yarn init -y
```

* Add a magic library for making things work.

```bash
yarn add @hornbill/cli -D
```

>Alternatively, your can install `@hornbill/cli` in global scope, thus `hornbill` command is available any where in your computer.

```bash
yarn global add @hornbill/cli
```

* Create a script file named `index.js`, so far your folder is something like:

```bash
 hornbill-test/
 ├── node_modules/
 │   └── ...
 ├── index.js
 ├── package.json
 └── yarn.lock
```

* Finally, we can write some code in `index.js`.

```js
import { RhinoApp } from "Rhino";

RhinoApp.WriteLine("Hello from Javascript");
```

* And, we can simply run this script by `yarn hornbill run index.js` command.
* Now, checkout you rhino command line, you can see `Hello from Javascript` in it.
