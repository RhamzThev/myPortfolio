import tkinter as tk
from tkinter import ttk

class View(ttk.Frame):

    def __init__(self, parent):
        super().__init__(parent)

        # INIT WIDGETS
        self.__init_widgets()

        self.controller = None
        
    # GETTERS AND SETTERS

    def set_controller(self, controller):
        self.controller = controller

    # INITIALIZATIONS
    def __init_widgets(self):
        ttk.Button(self, text="Win", command=self.__win).pack()
        ttk.Button(self, text="Lose", command=self.__lose).pack()

    # CONTROLLER FUNCTIONS

    def __win(self):
        if self.controller:
            self.controller.win()

    def __lose(self):
        if self.controller:
            self.controller.lose()