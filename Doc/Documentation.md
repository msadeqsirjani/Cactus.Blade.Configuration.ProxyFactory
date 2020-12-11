# Documentation

## Examples

The tests are some pretty good examples to get familiar with the lib. But here is the short summary on the Cactus.Blade.Caching API.

### Basic CRUD operations

#### Initialize

```C#
  // initialize, with default settings
  var storage = new Cactus.Blade.Caching();

  // ... or initialize with a custom configuration
  var config = new Cactus.Blade.CachingConfiguration() {
   // see the section "Configuration" further on
  };

  var storage = new Cactus.Blade.Caching(config);
```

##### Store()

```C#
    // store any object, or collection providing only a 'key'
    var key = "whatever";
    var value = "...";

    storage.Store(key, value);
```

##### Get()

```C#
    // fetch any object - as object
    storage.Get(key);

    // if you know the type, simply provide it as a generic parameter
    storage.Get<Animal>(key);
```

##### Query()

```C#
    // fetch a strong-typed collection
    storage.Query<Animal>(key);

    // you can also provide a strong-typed where-clause in one go
    storage.Query<Animal>(key, x => x.Name == "Sloth");
```

#### Other operations

##### Count

Returns the amount of items currently in the Cactus.Blade.Caching container.

##### Clear()

Clears the in-memory contents of the Cactus.Blade.Caching, but leaves any persisted state on disk intact.

##### Destroy()

Deletes the persisted file on disk, if it exists, but keeps the in-memory data intact.

##### Load()

Loads the persisted state from disk into memory, overriding the current memory instance. If the file does not exist, it simply does nothing.  
By default, this is done automatically at initialization and can be overriden by disabling `AutoLoad` in the configuration.

##### Persist()

Persists the in-memory store to disk.  
Note that by default, this is also done automatically when the Cactus.Blade.Caching disposes properly. This can be changed by disabling `AutoSave` in the configuration.

### Configuration

Here is a sample configuration with all configurable members (and their default values assigned):

- **AutoLoad** (bool)  
  Indicates if Cactus.Blade.Caching should automatically load previously persisted state from disk, when it is initialized (defaults to true).
- **AutoSave** (bool)  
  Indicates if Cactus.Blade.Caching should automatically persist the latest state to disk, on dispose (defaults to true).
- **Filename** (string)  
  Filename for the persisted state on disk (defaults to ".Cactus.Blade.Caching").

- **EnableEncryption**

Security is an important feature. Cactus.Blade.Caching has support for encrypting the data, both in-memory as well as persisted on disk.

You only need to define a custom configuration indication that encryption should be enabled:

```C#
    // setup a configuration with encryption enabled (defaults to 'false')
    // note that adding EncryptionSalt is optional, but recommended
    var config = new Cactus.Blade.CachingConfiguration() {
    	EnableEncryption = true,
    	EncryptionSalt = "(optional) add your own random salt string"
    };

    // initialize Cactus.Blade.Caching with a password of your choice
    var encryptedStorage = new Cactus.Blade.Caching(config, "password");
```

All write operations are first encrypted with AES, before they are persisted in-memory. In case of disk persistance, the encrypted value is respected. Although enabling encryption increases security, it does add a slight overhead to the Get/Store operations, in terms of performance.

By design, only the values are encrypted and not the keys.
