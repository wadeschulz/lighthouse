"""
Routes and views for the flask application.
"""

from datetime import datetime
from flask import render_template
from lighthouse import app

@app.route('/')
@app.route('/home')
def home():
    """Renders the home page."""
    return render_template(
        'index.jade',
        title='Typhoon - Lighthouse',
        year=2015
    )

@app.route('/annotate')
def annotate():
    """Renders the annotation page."""
    return render_template(
        'annotate.jade',
        title='Annotate',
        year=2015
    )