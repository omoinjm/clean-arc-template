# Testing Guide - Clean Architecture Template

This guide shows you all the ways to test that the Clean Architecture Template is working correctly.

## 🚀 Quick Start: 3-Minute Test

Copy and paste these commands to verify everything works:

```bash
# Navigate to project root
cd /path/to/clean-arc-template

# Test 1: Build (should show "Build succeeded")
dotnet build

# Test 2: Run Tests (should show "Passed: 2")
dotnet test

# Test 3: Verify it works
dotnet run --project src/Clean.Architecture.Template.API
```

Expected output:
- ✅ `Build succeeded` with 0 errors
- ✅ `Passed: 2` tests
- ✅ `Now listening on: http://localhost:5000`

---

## 📋 Detailed Testing Methods

### Method 1: Command Line Build Test

```bash
dotnet build
```

**Expected output:**
```
Build succeeded.
    0 Error(s)
```

**What it tests:**
- All 5 projects compile successfully
- Dependencies are resolved
- No syntax errors
- Project structure is valid

---

### Method 2: Unit Tests

```bash
dotnet test
```

**Expected output:**
```
Passed!  - Failed:     0, Passed:     2, Skipped:     0, Total:     2
```

**What it tests:**
- Test project is properly configured
- xUnit framework is working
- Moq mocking framework is functional
- All handler tests pass

**View detailed test output:**
```bash
dotnet test --verbosity detailed
```

---

### Method 3: Run the API Server

```bash
dotnet run --project src/Clean.Architecture.Template.API
```

**Expected output:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

**What it tests:**
- ASP.NET Core Web API starts successfully
- Dependency injection is configured
- MediatR is initialized
- AutoMapper is loaded
- Swagger is available

---

### Method 4: Test Health Endpoint

**Option A: Using cURL**

In a new terminal (while API is running):

```bash
curl https://localhost:5001/api/health/status
```

**Expected response:**
```json
{
  "status": "healthy",
  "timestamp": "2024-12-18T10:30:00Z"
}
```

**Option B: Using PowerShell**

```powershell
Invoke-WebRequest -Uri "https://localhost:5001/api/health/status" -UseBasicParsing
```

**Option C: Using browser**

Navigate to: `https://localhost:5001/api/health/status`

**What it tests:**
- HTTP routing works
- Controller is accessible
- Endpoint returns correct response
- Timestamp is generated

---

### Method 5: Swagger Documentation UI

**While API is running:**

1. Open browser: `https://localhost:5001/swagger`

**Expected:**
- Swagger UI loads successfully
- Health endpoint is visible
- "Try it out" button works
- Can execute endpoints from UI

**What it tests:**
- Swagger/OpenAPI is configured
- API documentation is generated
- Interactive documentation works

---

### Method 6: Verify Project Structure

```bash
# Check all projects exist
ls -la src/
ls -la tests/

# Verify DLL files were created
find . -name "*.dll" -path "*/bin/*" -type f
```

**Expected:**
- All 5 .csproj files exist
- All 5 bin/Debug folders created
- All .dll files present

---

### Method 7: Automated Test Script

```bash
# Make script executable (Linux/Mac)
chmod +x test-template.sh

# Run automated tests
./test-template.sh
```

**What it tests:**
- Entire solution builds
- All unit tests pass
- Project structure is complete
- Documentation exists
- DLL files are created

---

## 🔍 Troubleshooting Tests

### Issue: Build fails with errors

```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build
```

### Issue: Tests not running

```bash
# Restore test packages and run
dotnet test --no-restore --verbose

# Or clean and retry
dotnet clean
dotnet test
```

### Issue: API won't start

```bash
# Check if port is in use
netstat -ano | grep 5000

# Or use different port
dotnet run --project src/Clean.Architecture.Template.API -- --urls="https://localhost:5002"
```

### Issue: Swagger UI not loading

```bash
# Restart API
dotnet run --project src/Clean.Architecture.Template.API

# Check URL is correct: https://localhost:5001/swagger
# (Not 5000, but 5001)
```

---

## ✅ Full Testing Checklist

Complete this checklist to verify everything is working:

- [ ] **Build Test**
  ```bash
  dotnet build
  # Expect: 0 errors
  ```

- [ ] **Unit Tests**
  ```bash
  dotnet test
  # Expect: 2 tests passed
  ```

- [ ] **API Startup**
  ```bash
  dotnet run --project src/Clean.Architecture.Template.API
  # Expect: Server starts on port 5000/5001
  ```

- [ ] **Health Endpoint**
  ```bash
  curl https://localhost:5001/api/health/status
  # Expect: JSON response with status and timestamp
  ```

- [ ] **Swagger UI**
  - Open: https://localhost:5001/swagger
  - Verify: UI loads and shows endpoints

- [ ] **Project Files**
  - Verify 5 .csproj files exist
  - Check src/ and tests/ folders

- [ ] **Documentation**
  - [x] README.md exists
  - [x] CONTRIBUTING.md exists
  - [ ] docs/QUICKSTART.md exists
  - [ ] docs/API_DOCUMENTATION.md exists
  - [ ] docs/TESTING_GUIDE.md exists
  - [ ] docs/COMPLETION_SUMMARY.md exists

---

## 📊 Expected Results

### Build Output
```
Build succeeded.
    0 Error(s)
    4 Warning(s) (these are OK - just version warnings)
```

### Test Output
```
Passed!  - Failed:     0, Passed:     2, Skipped:     0, Total:     2
```

### API Output
```
Now listening on: http://localhost:5000
Now listening on: https://localhost:5001
```

### Health Endpoint Response
```json
{
  "status": "healthy",
  "timestamp": "2024-12-18T10:30:00Z"
}
```

---

## 🎯 Testing in CI/CD

For automated testing in GitHub Actions or other CI/CD:

```yaml
# Example GitHub Actions workflow
- name: Build
  run: dotnet build

- name: Test
  run: dotnet test

- name: Publish
  run: dotnet publish -c Release
```

---

## 🔗 Related Documentation

- [QUICKSTART.md](./QUICKSTART.md) - 5-minute setup
- [API_DOCUMENTATION.md](./API_DOCUMENTATION.md) - Feature creation
- [TESTING_GUIDE.md](./TESTING_GUIDE.md) - Testing options
- [COMPLETION_SUMMARY.md](./COMPLETION_SUMMARY.md) - What's included
- [../README.md](../README.md) - Architecture overview

---

## 💡 Tips

- **Fast rebuild:** `dotnet build --no-restore`
- **Quiet output:** Add `-q` flag (e.g., `dotnet build -q`)
- **Detailed output:** Add `--verbose` flag
- **Keep API running:** Use `dotnet run` and test in another terminal
- **SSL certificate issues:** The certificate is self-signed (expected in dev)

---

## ✨ Everything Working?

If all tests pass, your template is ready! Now:

1. Read [QUICKSTART.md](./QUICKSTART.md)
2. Follow [API_DOCUMENTATION.md](./API_DOCUMENTATION.md)
3. Create your first feature!

---

**Last Updated:** December 18, 2024  
**Status:** ✅ All tests passing
