from model import Model
from view import View

class Controller:
    def __init__(self, model: Model, view: View):
        self.model = model
        self.view = view

    def win(self):
        self.model.win()

    def lose(self):
        self.model.lose()