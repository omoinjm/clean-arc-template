#!/bin/bash

# Clean Architecture Template - Testing Script
# This script runs all tests to verify the template is working correctly

echo "╔════════════════════════════════════════════════════════════════╗"
echo "║  Clean Architecture Template - Automated Testing              ║"
echo "╚════════════════════════════════════════════════════════════════╝"
echo ""

# Color codes
GREEN='\033[0;32m'
RED='\033[0;31m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Test counters
PASSED=0
FAILED=0

# Function to print test result
test_result() {
    if [ $1 -eq 0 ]; then
        echo -e "${GREEN}✅ PASSED${NC}: $2"
        ((PASSED++))
    else
        echo -e "${RED}❌ FAILED${NC}: $2"
        ((FAILED++))
    fi
}

# Test 1: Clean solution
echo "📋 Test 1: Cleaning previous builds..."
dotnet clean -q 2>/dev/null
test_result $? "Clean previous builds"
echo ""

# Test 2: Build solution
echo "📋 Test 2: Building solution..."
BUILD_OUTPUT=$(dotnet build 2>&1)
BUILD_RESULT=$?
if [ $BUILD_RESULT -eq 0 ]; then
    ERROR_COUNT=$(echo "$BUILD_OUTPUT" | grep -c "error")
    if [ $ERROR_COUNT -eq 0 ]; then
        test_result 0 "Build solution (0 errors)"
    else
        test_result 1 "Build solution (found errors)"
    fi
else
    test_result 1 "Build solution"
fi
echo ""

# Test 3: Run unit tests
echo "📋 Test 3: Running unit tests..."
TEST_OUTPUT=$(dotnet test 2>&1)
TEST_RESULT=$?
PASSED_COUNT=$(echo "$TEST_OUTPUT" | grep -oP 'Passed:\s+\K\d+')
FAILED_COUNT=$(echo "$TEST_OUTPUT" | grep -oP 'Failed:\s+\K\d+')

if [ $TEST_RESULT -eq 0 ] && [ "$FAILED_COUNT" == "0" ]; then
    test_result 0 "Run unit tests ($PASSED_COUNT tests passed)"
else
    test_result 1 "Run unit tests ($FAILED_COUNT tests failed)"
fi
echo ""

# Test 4: Verify project structure
echo "📋 Test 4: Verifying project structure..."
PROJECTS=(
    "src/Clean.Architecture.Template.Core"
    "src/Clean.Architecture.Template.Application"
    "src/Clean.Architecture.Template.Infrastructure"
    "src/Clean.Architecture.Template.API"
    "tests/Clean.Architecture.Template.Application.Tests"
)

ALL_PROJECTS_EXIST=0
for project in "${PROJECTS[@]}"; do
    if [ ! -d "$project" ]; then
        echo -e "${RED}  Missing: $project${NC}"
        ALL_PROJECTS_EXIST=1
    fi
done

if [ $ALL_PROJECTS_EXIST -eq 0 ]; then
    test_result 0 "All 5 projects exist"
else
    test_result 1 "Project structure verification"
fi
echo ""

# Test 5: Verify documentation
echo "📋 Test 5: Verifying documentation..."
DOCS=(
    "README.md"
    "QUICKSTART.md"
    "API_DOCUMENTATION.md"
    "CONTRIBUTING.md"
    "COMPLETION_SUMMARY.md"
)

ALL_DOCS_EXIST=0
for doc in "${DOCS[@]}"; do
    if [ ! -f "$doc" ]; then
        echo -e "${RED}  Missing: $doc${NC}"
        ALL_DOCS_EXIST=1
    fi
done

if [ $ALL_DOCS_EXIST -eq 0 ]; then
    test_result 0 "All 5 documentation files present"
else
    test_result 1 "Documentation verification"
fi
echo ""

# Test 6: Check DLL files were created
echo "📋 Test 6: Verifying compiled DLL files..."
DLLS=(
    "src/Clean.Architecture.Template.Core/bin/Debug/net9.0/Clean.Architecture.Template.Core.dll"
    "src/Clean.Architecture.Template.Application/bin/Debug/net9.0/Clean.Architecture.Template.Application.dll"
    "src/Clean.Architecture.Template.Infrastructure/bin/Debug/net9.0/Clean.Architecture.Template.Infrastructure.dll"
    "src/Clean.Architecture.Template.API/bin/Debug/net9.0/Clean.Architecture.Template.API.dll"
    "tests/Clean.Architecture.Template.Application.Tests/bin/Debug/net9.0/Clean.Architecture.Template.Application.Tests.dll"
)

ALL_DLLS_EXIST=0
for dll in "${DLLS[@]}"; do
    if [ ! -f "$dll" ]; then
        ALL_DLLS_EXIST=1
    fi
done

if [ $ALL_DLLS_EXIST -eq 0 ]; then
    test_result 0 "All DLL files compiled successfully"
else
    test_result 1 "DLL compilation"
fi
echo ""

# Print summary
echo "╔════════════════════════════════════════════════════════════════╗"
echo "║                      TEST RESULTS SUMMARY                     ║"
echo "╚════════════════════════════════════════════════════════════════╝"
echo ""
echo -e "${GREEN}✅ Passed: $PASSED${NC}"
echo -e "${RED}❌ Failed: $FAILED${NC}"
echo ""

if [ $FAILED -eq 0 ]; then
    echo -e "${GREEN}🎉 ALL TESTS PASSED! Template is working perfectly!${NC}"
    echo ""
    echo "Next steps:"
    echo "  1. Read QUICKSTART.md to get started"
    echo "  2. Review API_DOCUMENTATION.md for tutorials"
    echo "  3. Start the API: dotnet run --project src/Clean.Architecture.Template.API"
    echo "  4. View Swagger: https://localhost:5001/swagger"
    exit 0
else
    echo -e "${RED}❌ Some tests failed. Please check the output above.${NC}"
    exit 1
fi
