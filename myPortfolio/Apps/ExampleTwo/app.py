import tkinter as tk
from tkinter import ttk

from model import Model
from view import View
from controller import Controller

class App(tk.Tk):

    def __init__(self):
        super().__init__()
        
        # INIT WINDOW
        self.__init_window()

        # MODEL
        model = Model()

        # VIEW
        view = View(self)
        view.pack()

        # CONTROLLER
        controller = Controller(model, view)

        # SET THA CONTROLLEA
        view.set_controller(controller)

    # INITIALIZATIONS
        
    def __init_window(self):
        self.title('Window')
        self.geometry('500x500')


def main():
    app = App()
    app.mainloop()

if __name__ == "__main__": main()