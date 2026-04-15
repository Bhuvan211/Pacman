#!/bin/bash

# Maze Hunter 3D - GitHub Push Helper Script
# This script will push your entire project to GitHub

echo "=========================================="
echo "Maze Hunter 3D - GitHub Push Setup"
echo "=========================================="
echo ""

# Check if repository exists on GitHub
echo "Checking GitHub repository..."
REPO_URL="https://github.com/Bhuvan211/Pacman.git"

# Try to access the repo
if ! git ls-remote "$REPO_URL" > /dev/null 2>&1; then
    echo ""
    echo "❌ Repository not found at: $REPO_URL"
    echo ""
    echo "Please create the repository on GitHub first:"
    echo "1. Go to: https://github.com/new"
    echo "2. Repository name: Pacman"
    echo "3. Description: Maze Hunter 3D - Game Development Project"
    echo "4. Choose Public or Private"
    echo "5. DO NOT initialize with README, .gitignore, or license"
    echo "6. Click 'Create repository'"
    echo ""
    echo "After creating the repository, run this script again."
    exit 1
fi

echo "✓ Repository found!"
echo ""
echo "=========================================="
echo "Pushing project to GitHub..."
echo "=========================================="
echo ""

# Push to GitHub
cd "/home/bhuvan/4th Sem/Game Dev/My project" || exit 1

git push -u origin master

if [ $? -eq 0 ]; then
    echo ""
    echo "=========================================="
    echo "✓ Project successfully pushed to GitHub!"
    echo "=========================================="
    echo ""
    echo "Repository URL: https://github.com/Bhuvan211/Pacman"
    echo ""
    echo "Your project is now live on GitHub!"
else
    echo ""
    echo "❌ Push failed. Make sure you have:"
    echo "1. Created the repository on GitHub"
    echo "2. Configured Git authentication (SSH or HTTPS)"
    echo ""
    echo "For HTTPS authentication:"
    echo "  git config --global user.email 'your-email@example.com'"
    echo "  git config --global user.name 'Your Name'"
    echo ""
    echo "Then run: git push -u origin master"
fi
